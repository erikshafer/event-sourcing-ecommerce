import { getEventStoreDBEventStore } from "@event-driven-io/emmett-esdb";
import { getApplication, startAPI } from "@event-driven-io/emmett-expressjs";
import { EventStoreDBClient } from "@eventstore/db-client";
import type { Application } from "express";
import { shoppingCartApi } from "./shoppingCarts/api";

const eventStoreDBClient = EventStoreDBClient.connectionString(
  `esdb://localhost:2113?tls=false`,
);
const eventStore = getEventStoreDBEventStore(eventStoreDBClient);

// eslint-disable-next-line @typescript-eslint/no-unused-vars
const getUnitPrice = (_productId: string) => {
  return Promise.resolve(100);
};

const shoppingCarts = shoppingCartApi(
  eventStore,
  getUnitPrice,
  () => new Date(),
);

const application: Application = getApplication({
  apis: [shoppingCarts],
});

// injecting a quick healthcheck for sanity checks
application.get("/health", (req, res) => {
  res.status(200).send("OK");
});

startAPI(application);
