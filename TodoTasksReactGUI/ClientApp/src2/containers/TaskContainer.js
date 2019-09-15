import { connect } from 'react-redux';
import React from 'react';
import TodoForm from '../components/TodoForm';
import { TodoList } from '../components/TodoList';
import { addTodo, updateTodo } from '../actions/index';

class TaskContainer extends React.Component {
    constructor(props) {
        super(props);
        
        this.state = { todoInput: this.getBaseTodo(), todoEditInput: null };
    }

    onEditTodo(id) {
        this.setState({ 
            selectedId: (this.state.selectedId === id) ? null : id,
            todoEditInput: {...this.props.todos.find(_ => _.id === id)}
         });
    }

    addNewTodo() {
        this.props.dispatch(addTodo(this.state.todoInput));
        this.resetForm();
    }

    editTodo() {
        this.props.dispatch(updateTodo(this.state.todoEditInput));
        this.cancelEditForm();
    }

    resetForm() {
        this.setState({ todoInput: this.getBaseTodo() });
    }

    cancelEditForm() {
        this.setState({ selectedId: null, todoEditInput: null });
    }

    handleInputChange(event) {
        const target = event.target;
        const value = target.type === 'checkbox' ? target.checked : target.value;
        const name = target.name;
        var todo = this.state.todoInput;
        todo[name] = value;
        
        this.setState({ todoInput: todo })
    }

    handleEditInputChange(event) {
        const target = event.target;
        const value = target.type === 'checkbox' ? target.checked : target.value;
        const name = target.name;
        var todo = this.state.todoEditInput;
        todo[name] = value;
        
        this.setState({ todoEditInput: todo })
    }

    getBaseTodo() {
        return {
            id: '',
            todoName: '',
            reason: '',
            status: '',
            type: '',
            description: ''
        };
    }

    render() {
        return (
            <section className="container">
                <div className="card mt-2" style={{maxWidth: '40rem'}}>
                    <h5 className="card-header">Todos</h5>   
                    <TodoList selectedId={this.state.selectedId} todos={this.props.todos} editTodo={this.onEditTodo.bind(this)}>
                    {isActive => (
                        isActive
                        ? (
                        <div className="card">
                            <div className="card-body">
                                <TodoForm 
                                    todo={this.state.todoEditInput}
                                    handleInputChange={this.handleEditInputChange.bind(this)}
                                    referenceData={this.props.referenceData} 
                                    submitForm={this.editTodo.bind(this)}
                                    resetForm={this.cancelEditForm.bind(this)}
                                ></TodoForm>
                            </div>
                        </div>
                        )
                        : null
                    )}
                    </TodoList> 
                    
                    <div className="card-body">
                        <TodoForm 
                            todo={this.state.todoInput}
                            handleInputChange={this.handleInputChange.bind(this)}
                            referenceData={this.props.referenceData} 
                            submitForm={this.addNewTodo.bind(this)}
                            resetForm={this.resetForm.bind(this)}
                        ></TodoForm>
                    </div>
                </div>
            </section>
        )
    } 
}

const mapStateToProps = state => {
    return { referenceData: state.referenceDataReducer.referenceData, todos: state.todoReducer.todos };
}

export default connect(mapStateToProps)(TaskContainer);