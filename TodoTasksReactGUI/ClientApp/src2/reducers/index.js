import { combineReducers } from 'redux'
import todoReducer from './todo';
import referenceDataReducer from './referenceData';

export default combineReducers({
    todoReducer,
    referenceDataReducer
});