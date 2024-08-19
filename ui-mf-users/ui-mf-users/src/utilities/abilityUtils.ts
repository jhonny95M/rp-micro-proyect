import { Action } from "../types/action";
import { Menu } from "../types/menu";
import { Profile } from "../types/profile";
import Ability from "./ability";
import { Actions, Subjects } from "./types";

interface RouteAction{
    route:string;
    actions:Action[];
}

export const extractActions = (menu: Menu): RouteAction[] => {
    let routeActions: RouteAction[] = [];
    if (menu.action) {
        routeActions.push({route: menu.route, actions: menu.action || []});
    }
    if (menu.child) {
        menu.child.forEach((child: Menu) => {
            routeActions =routeActions.concat(extractActions(child));
        });
    }
    return routeActions;
}
export const defineAbilitiesFor = (data: Profile): any => {
    const allRouteActions = data.menu.reduce((acc: RouteAction[], menu: Menu) => {
        return acc.concat(extractActions(menu));
    }, []);
    const rules = allRouteActions
                    .flatMap((routeAction: RouteAction) => 
                        routeAction.actions.map((action: Action) => ({
                            action: action.actionName?.split('-')[0] as Actions,
                            subject: action.actionName?.split('-')[1] as Subjects,
                            route: routeAction.route
                        }))
    );
    console.log("rules", rules);
    return new Ability(rules);
}