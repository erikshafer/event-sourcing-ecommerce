import { getEventStoreDBEventStore } from "@event-driven-io/emmett-esdb";
import { getApplication, startAPI } from "@event-driven-io/emmett-expressjs";
import { EventStoreDBClient } from "@eventstore/db-client";
import { shoppingCartApi } from "./shoppingCarts/api";
const eventStoreDBClient = EventStoreDBClient.connectionString(`esdb://localhost:2113?tls=false`);
const eventStore = getEventStoreDBEventStore(eventStoreDBClient);
const getUnitPrice = (_productId) => {
    return Promise.resolve(100);
};
const shoppingCarts = shoppingCartApi(eventStore, getUnitPrice, () => new Date());
const application = getApplication({
    apis: [shoppingCarts],
});
startAPI(application);
