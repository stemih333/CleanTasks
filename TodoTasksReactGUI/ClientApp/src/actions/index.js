import { ADD_REFERENCE_DATA } from "../constants/actions";

export function addReferenceData(referenceData) {
    return { type: ADD_REFERENCE_DATA, referenceData };
}

