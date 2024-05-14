/* eslint-disable @typescript-eslint/no-floating-promises */
import {} from "@event-driven-io/emmett";
import { getEventStoreDBEventStore } from "@event-driven-io/emmett-esdb";
import { ApiE2ESpecification, expectResponse, getApplication, } from "@event-driven-io/emmett-expressjs";
import { EventStoreDBContainer, StartedEventStoreDBContainer, } from "@event-driven-io/emmett-testcontainers";
import { randomUUID } from "node:crypto";
import { after, before, beforeEach, describe, it } from "node:test";
import { shoppingCartApi } from "./api";
import {} from "./shoppingCart";
const getUnitPrice = () => {
    return Promise.resolve(100);
};
describe("ShoppingCart E2E", () => {
    let clientId;
    let shoppingCartId;
    let esdbContainer;
    let given;
    before(async () => {
        esdbContainer = await new EventStoreDBContainer().start();
        given = ApiE2ESpecification.for(() => getEventStoreDBEventStore(esdbContainer.getClient()), (eventStore) => getApplication({
            apis: [shoppingCartApi(eventStore, getUnitPrice, () => now)],
        }));
    });
    beforeEach(() => {
        clientId = randomUUID();
        shoppingCartId = `shopping_cart:${clientId}:current`;
    });
    after(() => {
        return esdbContainer.stop();
    });
    describe("When empty", () => {
        it("should add product item", () => {
            return given((request) => request
                .post(`/clients/${clientId}/shopping-carts/current/product-items`)
                .send(productItem))
                .when((request) => request.get(`/clients/${clientId}/shopping-carts/current`).send())
                .then([
                expectResponse(200, {
                    body: {
                        clientId,
                        id: shoppingCartId,
                        productItems: [
                            {
                                quantity: productItem.quantity,
                                productId: productItem.productId,
                            },
                        ],
                        status: "Opened",
                    },
                }),
            ]);
        });
    });
    const now = new Date();
    const getRandomProduct = () => {
        return {
            productId: randomUUID(),
            unitPrice: 100,
            quantity: Math.random() * 10,
        };
    };
    const productItem = getRandomProduct();
});
