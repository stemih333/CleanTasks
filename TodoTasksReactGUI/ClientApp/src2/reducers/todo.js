import { ADD_TODO, UPDATE_TODO } from "../constants/actions";

const initialState = {
    todos: [
    { 
        id: 1,
        todoName: 'test1',
        reason: '1',
        status: '1',
        type: '0',
        description: 'descr 1' 
    },
    { 
        id: 2,
        todoName: 'test2',
        reason: '2',
        status: '2',
        type: '1',
        description: 'descr 2' 
    }]
}

export default (state = initialState, action) => {
    if (action.type === ADD_TODO) {
        return { ...state, todos: [...state.todos, action.todo] };
    } else if (action.type === UPDATE_TODO) {
        return { ...state, todos: state.todos.map(_ => {
            return (_.id === action.todo.id) ? action.todo : _;
        })};
    }

    return state;
}