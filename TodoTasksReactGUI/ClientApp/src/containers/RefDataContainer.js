import React from 'react';
import { RefDataList } from '../components/RefDataList';

export default class RefDataContainer extends React.Component {
    constructor(props) {
        super(props);
        
        this.state = { selectedRefData: props.refData[0] }
    }

    selectRefData(refDataItem) {
        this.setState({ selectedRefData: refDataItem });
    }

    render() {
        return (
            <section className="container">
                <RefDataList refKeys={Object.keys(this.props.refData)}></RefDataList>
            </section>
        )
    }
}