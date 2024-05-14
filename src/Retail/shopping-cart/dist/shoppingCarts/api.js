import { assertNotEmptyString, assertPositiveNumber, CommandHandler, } from "@event-driven-io/emmett";
import { NoContent, NotFound, OK, on, } from "@event-driven-io/emmett-expressjs";
import {} from "express";
import { addProductItem, cancel, confirm, removeProductItem, } from "./businessLogic";
import { evolve, getInitialState, } from "./shoppingCart";
export const handle = CommandHandler(evolve, getInitialState);
export const getShoppingCartId = (clientId) => `shopping_cart:${assertNotEmptyString(clientId)}:current`;
export const shoppingCartApi = (eventStore, getUnitPrice, getCurrentTime) => (router) => {
    // Add Product Item
    router.post("/clients/:clientId/shopping-carts/current/product-items", on(async (request) => {
        const shoppingCartId = getShoppingCartId(assertNotEmptyString(request.params.clientId));
        const productId = assertNotEmptyString(request.body.productId);
        const command = {
            type: "AddProductItemToShoppingCart",
            data: {
                shoppingCartId,
                productItem: {
                    productId,
                    quantity: assertPositiveNumber(request.body.quantity),
                    unitPrice: await getUnitPrice(productId),
                },
            },
            metadata: { now: getCurrentTime() },
        };
        await handle(eventStore, shoppingCartId, (state) => addProductItem(command, state));
        return NoContent();
    }));
    // Remove Product Item
    router.delete("/clients/:clientId/shopping-carts/current/product-items", on(async (request) => {
        const shoppingCartId = getShoppingCartId(assertNotEmptyString(request.params.clientId));
        const command = {
            type: "RemoveProductItemFromShoppingCart",
            data: {
                shoppingCartId,
                productItem: {
                    productId: assertNotEmptyString(request.query.productId),
                    quantity: assertPositiveNumber(Number(request.query.quantity)),
                    unitPrice: assertPositiveNumber(Number(request.query.unitPrice)),
                },
            },
            metadata: { now: getCurrentTime() },
        };
        await handle(eventStore, shoppingCartId, (state) => removeProductItem(command, state));
        return NoContent();
    }));
    // Confirm Shopping Cart
    router.post("/clients/:clientId/shopping-carts/current/confirm", on(async (request) => {
        const shoppingCartId = getShoppingCartId(assertNotEmptyString(request.params.clientId));
        const command = {
            type: "ConfirmShoppingCart",
            data: { shoppingCartId },
            metadata: { now: getCurrentTime() },
        };
        await handle(eventStore, shoppingCartId, (state) => confirm(command, state));
        return NoContent();
    }));
    // Cancel Shopping Cart
    router.delete("/clients/:clientId/shopping-carts/current", on(async (request) => {
        const shoppingCartId = getShoppingCartId(assertNotEmptyString(request.params.clientId));
        const command = {
            type: "CancelShoppingCart",
            data: { shoppingCartId },
            metadata: { now: getCurrentTime() },
        };
        await handle(eventStore, shoppingCartId, (state) => cancel(command, state));
        return NoContent();
    }));
    // Get Shopping Cart
    router.get("/clients/:clientId/shopping-carts/current", on(async (request) => {
        const shoppingCartId = getShoppingCartId(assertNotEmptyString(request.params.clientId));
        const result = await eventStore.aggregateStream(shoppingCartId, {
            evolve,
            getInitialState,
        });
        if (result === null)
            return NotFound();
        if (result.state.status !== "Opened")
            return NotFound();
        const productItems = [...result.state.productItems].map(([productId, quantity]) => ({
            productId,
            quantity,
        }));
        return OK({
            body: {
                clientId: assertNotEmptyString(request.params.clientId),
                id: shoppingCartId,
                productItems,
                status: result.state.status,
            },
        });
    }));
};
