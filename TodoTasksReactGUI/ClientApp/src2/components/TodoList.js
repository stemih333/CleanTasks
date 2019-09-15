import React from 'react';

export const TodoList = ({todos, editTodo, selectedId, children}) => {
    return (
        <ul className="list-group list-group-flush">
            {
                todos.map(_ => 
                <li key={_.id}                     
                    className="list-group-item ">
                    <div onClick={() => editTodo(_.id)}>{_.todoName}</div>
                    
                     
                    <div>
                        {children(_.id === selectedId)}
                    </div>                
                </li>)
            }
        </ul>
    );
}