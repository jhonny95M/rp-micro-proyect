import { Action } from "../types/Action";
import { Menu } from "../types/menu";
import { Profile } from "../types/profile";
import Ability from "./ability";
import { Rule, Actions, Subjects } from "./types";



export const extractActions = (menu: Menu): Action[] => {
    let actions: Action[] = [];
    if (menu.action) {
        actions = actions.concat(menu.action);
    }
    if (menu.child) {
        menu.child.forEach((child: Menu) => {
            actions = actions.concat(extractActions(child));
        });
    }
    return actions;
}
export const defineAbilitiesFor = (data: Profile): any => {
    const allActions = data.menu.reduce((acc: Action[], menu: Menu) => {
        return acc.concat(extractActions(menu));
    }, []);
    const rules = allActions.map((action: Action) => ({
        action: action.actionName?.split('-')[0] as Actions,
        subject: action.actionName?.split('-')[1] as Subjects,
    }));
    console.log("rules", rules);
    return new Ability(rules);
}