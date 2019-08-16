import React from 'react';

export const RefDataList = (props) => {
    const items = props.refKeys.map(_ => (<li className="list-group-item" key={_}>{_}</li>));

    return <section className="card">
        <div className="card-header">
            Reference Data
        </div>
        <ul className="list-group list-group-flush">
            {items}
        </ul>
    </section>
}