<template>
  <div>
    <div>
      <Container :drop-placeholder="{ className: 'placeholder' }" class="board">
        <Draggable v-for="container in containers" :key="container.id">
          <div class="card Card">
            <header class="card-header">
              <p class="card-header-title">
                <LabelEdit
                  :text="container.name"
                  @updatedText="textUpdated($event, container)"
                ></LabelEdit>
              </p>

              <div class="navbar-item has-dropdown is-hoverable">
                <a class="navbar-link is-arrowless">
                  <img
                    src="../assets/menu.png"
                    alt="Minha Figura"
                    class="image is-24x24"
                  />
                </a>

                <div class="navbar-dropdown is-right">
                  <ModalConfirm
                    @confirm="confirmDelete($event, container)"
                  ></ModalConfirm
                  ><!--// Item de Deletar -->

                  <a
                    @click="MovContainerDireita(container)"
                    class="navbar-item"
                  >
                    Mover para direita
                  </a>
                  <a
                    @click="MovContainerEsquerda(container)"
                    class="navbar-item"
                  >
                    mover para esquerda
                  </a>
                </div>
              </div>
            </header>
            <div class="lane">
              <div>
                <Container
                  group-name="container"
                  @drag-start="DragStart(container, $event)"
                  @drop="DropEnd(container, $event)"
                  :get-child-payload="getChildPayload"
                  :drop-placeholder="{ className: 'placeholder' }"
                >
                  <Draggable v-for="card in container.cards" :key="card.id">
                    <Card>
                      <Modal
                        :card="card"
                        @close="closeModal($event, card, container)"
                      ></Modal>
                    </Card>
                  </Draggable>
                </Container>
              </div>
            </div>
            <footer class="card-footer">
              <a @click="newCard(container)" class="card-footer-item"
                >Adicionar Cartão</a
              >
            </footer>
          </div>
        </Draggable>

        <div class="card Card">
          <header class="card-header">
            <a
              type="button"
              @click="newContainer"
              class="card-header-title btn"
            >
              Adicionar Coluna +
            </a>
          </header>
        </div>
      </Container>
    </div>
  </div>
</template>

<script>
import Card from "../components/Card.vue";
import LabelEdit from "../components/LabelEdit.vue";
import Modal from "../components/Modal.vue";
import ModalConfirm from "../components/ModalConfirm.vue";
import { Container, Draggable } from "vue-smooth-dnd";

import axios from "axios";

export default {
  data() {
    return {
      User: {},
      containers: [],
      draggingCard: {
        container: "",
        index: -1,
        cardData: {},
      },
    };
  },
  created: async function () {
    this.User = JSON.parse(localStorage.getItem("user"));
    if (this.User == null) {
      this.$router.push("/");
      alert("Realize o Login para acessar a página de Quadros!");
    } else {
      await axios
        .get("https://localhost:44360/Containers/filter/" + this.User.id)
        .then((res) => {
          this.containers = res.data;
          this.containers.sort(this.compare);
          this.containers.forEach((container) => {
            axios
              .get("https://localhost:44360/Cards/filter/" + container.id)
              .then((res) => {
                container.cards = res.data;
                container.cards.sort(this.compare);
              })
              .catch((err) => {
                alert(err);
              });
          });
        })
        .catch((err) => {
          console.log(err);
        });
    }
  },
  components: {
    LabelEdit,
    Card,
    Container,
    Draggable,
    Modal,
    ModalConfirm,
  },
  methods: {
    DragStart: function (container, dragResult) {
      // faz a troca dos cards ( quando segura)
      const { payload, isSource } = dragResult;
      if (isSource) {
        this.draggingCard = {
          container,
          index: payload.index,
          cardData: {
            ...this.containers[container.position].cards[payload.index],
          },
        };
      }
    },
    DropEnd: function (container, dropResult) {
      // faz a troca dos cards ( quando solta)
      const { removedIndex, addedIndex } = dropResult;
      if (
        container === this.draggingCard.container &&
        removedIndex === addedIndex
      ) {
        return;
      }

      if (removedIndex !== null) {
        container.cards.splice(removedIndex, 1);
        this.updatePosition(removedIndex, addedIndex, container);
      }
      if (addedIndex !== null) {
        this.draggingCard.cardData.containerID = container.id;
        container.cards.splice(addedIndex, 0, this.draggingCard.cardData);
        this.updatePosition(removedIndex, addedIndex, container);
      }
    },
    getChildPayload: function (index) {
      return {
        index,
      };
    },
    textUpdated: function ($event, container) {
      //Serve para atualizar o Name do container
      container.name = $event;
      this.updateContainer(container);
    },
    newCard: function (container) {
      // cria uma nova
      var card = {
        name: "Cartão sem título ",
        position: container.cards.length,
        description: "",
        containerID: container.id,
      };
      axios
        .post("https://localhost:44360/Cards", card)
        .then((res) => {
          card.id = res.data.id;
          container.cards.push(card);
        })
        .catch((err) => {
          console.log(err);
        });
    },
    newContainer: function () // cria um novo container
    {
      var container = {
        name: "Container sem Título",
        id: 0,
        idUser: this.User.id,
        position: this.containers.length,
        cards: [],
      };
      axios
        .post("https://localhost:44360/Containers", container)
        .then((res) => {
          container.id = res.data.id;
          this.containers.push(container);
        })
        .catch((err) => {
          console.log(err);
        });
    },
    closeModal: function (
      $event,
      card,
      container //Atualiza o Card quando acessado ou deleta ele
    ) {
      if ($event.isDelete == true) {
        var index = container.cards.indexOf(card);
        container.cards.splice(index, 1);
        this.updatePosition(index, null, container);
        this.deleteCard(card);
      } else {
        card = $event.card;
        this.updateCard(card);
      }
    },
    confirmDelete: function ($event, container) {
      if ($event) {
        var index = this.containers.indexOf(container);
        this.containers.splice(index, 1);
        for (index; index < this.containers.length; index++) {
          this.containers[index].position--;
          this.updateContainer(this.containers[index]);
        }
        this.deleteContainer(container);
      }
    },

    updatePosition: function (
      removedIndex,
      addedIndex,
      container //Atualiza a posição do Card.
    ) {
      if (removedIndex !== null && addedIndex !== null) {
        var menor = removedIndex < addedIndex ? removedIndex : addedIndex;
        for (; menor < container.cards.length; menor++) {
          container.cards[menor].position = menor;
          this.updateCard(container.cards[menor]);
        }
      } else {
        var i = 0;
        if (removedIndex !== null) {
          for (i = removedIndex; i < container.cards.length; i++) {
            container.cards[i].position = i;
            this.updateCard(container.cards[i]);
          }
        }
        if (addedIndex !== null) {
          for (i = addedIndex; i < container.cards.length; i++) {
            container.cards[i].position = i;

            this.updateCard(container.cards[i]);
          }
        }
      }
    },
    MovContainerDireita: function (container) {
      if (container.position != this.containers.length - 1) {
        this.containers[container.position + 1].position--;
        container.position++;
        this.updateContainer(container);
        this.updateContainer(this.containers[container.position]);
        this.containers.sort(this.compare);
      }
    },
    MovContainerEsquerda: function (container) {
      if (container.position != 0) {
        this.containers[container.position - 1].position++;
        container.position--;

        this.updateContainer(container);
        this.updateContainer(this.containers[container.position]);
        this.containers.sort(this.compare);
      }
    },
    updateCard: function (card) {
      axios
        .put("https://localhost:44360/Cards/" + card.id, card)
        .then()
        .catch((err) => {
          console.log(err);
        });
    },
    updateContainer: function (container) {
      axios
        .put("https://localhost:44360/Containers/" + container.id, container)
        .then()
        .catch((err) => {
          console.log(err);
        });
    },
    deleteCard: function (card) {
      axios
        .delete("https://localhost:44360/Cards/" + card.id)
        .then()
        .catch((err) => {
          console.log(err);
        });
    },
    deleteContainer: function (container) {
      container.cards.forEach((card) => {
        this.deleteCard(card);
      });
      axios
        .delete("https://localhost:44360/Containers/" + container.id)
        .then()
        .catch((err) => {
          console.log(err);
        });
    },
    compare: function (a, b) {
      // serve para ordenar a lista pela posição
      if (a.position > b.position) return 1;
      if (b.position > a.position) return -1;

      return 0;
    },
  },
};
</script>

<style>
.text {
  text-align: left;
}

.btn:hover {
  background-color: #ededed;
}

.board {
  margin-top: 1%;
  display: flex;
  justify-content: flex-start;
  align-items: flex-start;
}

.Card {
  background-color: #d7d7d7;
  margin-left: 1.5%;
}

.placeholder {
  background: #cecece;
  border-radius: 0.4rem;
  transform: scaleY(0.85);
  transform-origin: 0% 0%;
}
.avoid-clicks {
  pointer-events: none;
}
.lane {
  max-width: 100%;
  width: 22rem;
  box-shadow: 0 0.1rem 0.2rem 0 #dcdcdc;
  margin: 4% 0.5%;
}
</style>