import { ADD_REFERENCE_DATA } from "../constants/actions";

const initialState = {
    referenceData: null
}

export default (state = initialState, action) => {
    if(action.type === ADD_REFERENCE_DATA) {
        return { ...state, referenceData: action.referenceData };
    } 

    return state;
}