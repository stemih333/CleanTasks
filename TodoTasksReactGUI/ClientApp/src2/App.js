import React from 'react';
import './App.css';
import AppNavbar from './components/AppNavbar';
import 'bootstrap/dist/css/bootstrap.min.css';
import { connect } from "react-redux";
import { getReferenceData } from './actions/index';
import TaskContainer from './containers/TaskContainer';
import { BrowserRouter, Route, Switch } from 'react-router-dom';

function Test(props) {
  console.log(props)
  return <h1>Test</h1>
}

function Test2() {
  return <h1>Test2</h1>
}


class AppComponent extends React.Component {
  componentDidMount() {
    this.props.dispatch(getReferenceData());
  }

  render() {
    const elem = this.props.referenceData ? <TaskContainer></TaskContainer>  : <div>Loading...</div>
    return (
      <BrowserRouter >
        <main>
          <AppNavbar></AppNavbar>
          {elem}
          <Switch>
            <Route exact path="/" component={Test2} />
            <Route path="/test" component={Test} />
          </Switch>

        </main>
      </BrowserRouter>
      
    );
  } 
}




const mapStateToProps = state => {
  return { referenceData: state.referenceDataReducer.referenceData, todos: state.todoReducer.todos };
}

const App = connect(mapStateToProps)(AppComponent);

export default App;
