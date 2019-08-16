import React from 'react';

export const RefDataView = (props) => {
    var items = props.selectedRefData.map(_ => <li key={_.id} className="list-group-item">{_.name}</li>);

    return <section className="card">
        <div className="card-header">
            {props.name}
        </div>
        <ul className="list-group list-group-flush">
            {items}
        </ul>
    </section>
}