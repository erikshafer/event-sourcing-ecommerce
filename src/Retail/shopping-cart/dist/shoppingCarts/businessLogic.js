import { EmmettError, IllegalStateError, sum, } from "@event-driven-io/emmett";
import { evolve, getInitialState, } from "./shoppingCart";
/////////////////////////////////////////
////////// Business Logic
/////////////////////////////////////////
export const addProductItem = (command, state) => {
    if (state.status === "Closed")
        throw new IllegalStateError("Shopping Cart already closed");
    const { data: { shoppingCartId, productItem }, metadata, } = command;
    return {
        type: "ProductItemAddedToShoppingCart",
        data: {
            shoppingCartId,
            productItem,
            addedAt: metadata?.now ?? new Date(),
        },
    };
};
export const removeProductItem = (command, state) => {
    if (state.status !== "Opened")
        throw new IllegalStateError("Shopping Cart is not opened");
    const { data: { shoppingCartId, productItem }, metadata, } = command;
    const currentQuantity = state.productItems.get(productItem.productId) ?? 0;
    if (currentQuantity < productItem.quantity)
        throw new IllegalStateError("Not enough products");
    return {
        type: "ProductItemRemovedFromShoppingCart",
        data: {
            shoppingCartId,
            productItem,
            removedAt: metadata?.now ?? new Date(),
        },
    };
};
export const confirm = (command, state) => {
    if (state.status !== "Opened")
        throw new IllegalStateError("Shopping Cart is not opened");
    const totalQuantityOfAllProductItems = sum(state.productItems.values());
    if (totalQuantityOfAllProductItems <= 0)
        throw new IllegalStateError("Shopping Cart is empty");
    const { data: { shoppingCartId }, metadata, } = command;
    return {
        type: "ShoppingCartConfirmed",
        data: {
            shoppingCartId,
            confirmedAt: metadata?.now ?? new Date(),
        },
    };
};
export const cancel = (command, state) => {
    if (state.status !== "Opened")
        throw new IllegalStateError("Shopping Cart is not opened");
    const { data: { shoppingCartId }, metadata, } = command;
    return {
        type: "ShoppingCartCancelled",
        data: {
            shoppingCartId,
            canceledAt: metadata?.now ?? new Date(),
        },
    };
};
export const decide = (command, state) => {
    const { type } = command;
    switch (type) {
        case "AddProductItemToShoppingCart":
            return addProductItem(command, state);
        case "RemoveProductItemFromShoppingCart":
            return removeProductItem(command, state);
        case "ConfirmShoppingCart":
            return confirm(command, state);
        case "CancelShoppingCart":
            return cancel(command, state);
        default: {
            const _notExistingCommandType = type;
            throw new EmmettError(`Unknown command type`);
        }
    }
};
export const decider = {
    decide,
    evolve,
    getInitialState,
};
