import React from 'react';
import './App.css';
import AppNavbar from './components/AppNavbar';
import 'bootstrap/dist/css/bootstrap.min.css';
import { connect } from "react-redux";
import { getReferenceData } from './middleware/referenceData';
import { BrowserRouter, Route, Switch } from 'react-router-dom';


function Home() {
  return <h1>Home</h1>;
}

function Tasks() {
  return <h1>Tasks</h1>;
}

function Permissions() {
  return <h1>Permissions</h1>;
}

function UserDetails() {
  return <h1>User details</h1>;
}

function NoMatch() {
  return <h1>404</h1>;
}

class AppComponent extends React.Component {
  componentDidMount() {
    this.props.dispatch(getReferenceData());
  }

  render() {
    const elem = this.props.referenceData ? <Main></Main>  : <div>Loading...</div>
    return (
      <BrowserRouter >
        <main>
          <AppNavbar></AppNavbar>
          {elem}
        </main>
      </BrowserRouter>     
    );
  } 
}

const Main = () => {
  return (
    <Switch>
      <Route path="/" exact component={Home} />
      <Route path="/tasks" component={Tasks} />
      <Route path="/permissions" component={Permissions} />
      <Route path="/details" component={UserDetails} />
      <Route component={NoMatch}/>
    </Switch>
  );
}


const mapStateToProps = state => {
  return { referenceData: state.referenceData.referenceData };
}

const App = connect(mapStateToProps)(AppComponent);

export default App;
