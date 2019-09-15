import { ADD_REFERENCE_DATA, ADD_TODO, DELETE_TODO, UPDATE_TODO } from "../constants/actions";

let itemId = 2; 

export function addReferenceData(referenceData) {
    return { type: ADD_REFERENCE_DATA, referenceData };
}

export function addTodo(todo) {
    todo.id = ++itemId;
    return { type: ADD_TODO, todo };
}

export function updateTodo(todo) {
    return { type: UPDATE_TODO, todo };
}

export function deleteTodo(id) {
    return { type: DELETE_TODO, id };
}

export const getReferenceData = () => {  
    return async (dispatch) => {
        const res = await fetch('/api/ReferenceData', {
            headers: { 'Content-Type': 'application/json' },
            credentials: 'include'
        });
        const json = await res.json();

        return dispatch(addReferenceData(json));
    }
}