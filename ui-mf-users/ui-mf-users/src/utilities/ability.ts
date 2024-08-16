import { useRoute } from '../Context/RouteContext';
import { Actions, Subjects, Rule } from './types';

class Ability {
  private rules: Rule[];

  constructor(rules: Rule[]) {
    this.rules = rules;
  }

  can(action: Actions, subject: Subjects, route?: string): boolean {
    const currentRoute =`${route || useRoute()}` ;
    console.log("currentRoute", currentRoute);
    const ruleExists= this.rules.some(rule => {
      if (rule.action !== action && rule.action !== 'manage') return false;
      if (rule.subject !== subject && rule.subject !== 'all') return false;
      if(currentRoute && rule.route !== currentRoute) return false;
      return true;
    });
    return ruleExists;
  }
}

export default Ability;