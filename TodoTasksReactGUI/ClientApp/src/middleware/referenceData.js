import { addReferenceData } from '../actions/index';

export const getReferenceData = () => {  
    return async (dispatch) => {
        const res = await fetch('/api/ReferenceData', {
            headers: { 'Content-Type': 'application/json' },
            credentials: 'include'
        });
        const json = await res.json();

        return dispatch(addReferenceData(json));
    }
}