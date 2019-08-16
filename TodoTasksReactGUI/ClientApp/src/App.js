import React from 'react';
import './App.css';
import AppNavbar from './components/AppNavbar';
import 'bootstrap/dist/css/bootstrap.min.css';
import RefDataContainer from './containers/RefDataContainer';

class App extends React.Component {
  constructor(props) {
    super(props);
    this.state = { referenceData: null };
    this.initData = this.initData.bind(this);
  }

  componentDidMount() {
    this.initData();
  }

  initData() {
    fetch('/api/ReferenceData' ,{ 
      headers: { 'Content-Type': 'application/json' },
      credentials: 'include'
    }).then(res => { 
      res.json().then(json => this.setState({ referenceData: json })); 
    });
  }

  render() {
    const elem = this.state.referenceData ? <RefDataContainer refData={this.state.referenceData}></RefDataContainer>  : <div>Loading...</div>
    return (
      <main>
        <AppNavbar></AppNavbar>
        {elem}
      </main>
    );
  }
  
}

export default App;
