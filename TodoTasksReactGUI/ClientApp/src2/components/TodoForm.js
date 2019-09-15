import React from 'react';

export default class TodoForm extends React.Component {
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

    getOptions(items) {
        return items.map(_ => <option key={_.id} value={_.id}>{_.name}</option>)
    }

    onSubmitForm(evt) {
        evt.preventDefault();
        this.props.submitForm();
    }

    render() {
        return (
            <form onSubmit={this.onSubmitForm.bind(this)}>
                <div className="form-row mb-2">
                    <label className="col-form-label col-3" >Todo name: </label>
                    <input className="form-control col-8" maxLength="10" required name="todoName" value={this.props.todo.todoName} onChange={this.props.handleInputChange} />
                </div>
                <div className="form-row mb-2">
                    <label className="col-form-label col-3">Reason: </label>
                    <select className="form-control col-8" required name="reason" value={this.props.todo.reason} onChange={this.props.handleInputChange}>
                        <option></option>
                        {this.getOptions(this.props.referenceData.reasons)}
                    </select>
                </div>
                <div className="form-row mb-2">
                    <label className="col-form-label col-3">Status: </label>
                    <select className="form-control col-8" required name="status" value={this.props.todo.status} onChange={this.props.handleInputChange}>
                        <option></option>
                        {this.getOptions(this.props.referenceData.statuses)}
                    </select>
                </div>
                <div className="form-row mb-2">
                    <label className="col-form-label col-3">Type: </label>
                    <select className="form-control col-8" required name="type" value={this.props.todo.type} onChange={this.props.handleInputChange}>
                        {this.getOptions(this.props.referenceData.types)}
                    </select>
                </div>
                <div className="form-row mb-2">
                    <label className="col-form-label col-3">Description: </label>
                    <textarea rows="5" className="form-control col-8" name="description" value={this.props.todo.description} onChange={this.props.handleInputChange} />
                </div>
                <div className="text-right">
                    <button type="button" onClick={this.props.resetForm} className="btn btn-sm btn-danger mr-2">Cancel</button>
                    <button type="submit" className="btn btn-sm btn-primary">Submit</button>
                </div>
            </form>
        )
    }
}