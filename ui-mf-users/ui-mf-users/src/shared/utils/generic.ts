/* eslint-disable @typescript-eslint/explicit-function-return-type */
/* eslint-disable prettier/prettier */


export const upperCaseFirst = (word: string) => {
    const capitalized = word.charAt(0).toUpperCase() + word.slice(1);
    return capitalized;
}