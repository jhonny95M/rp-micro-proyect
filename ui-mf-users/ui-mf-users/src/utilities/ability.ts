import { Actions, Subjects, Rule } from './types';

class Ability {
  private rules: Rule[];

  constructor(rules: Rule[]) {
    this.rules = rules;
  }

  can(action: Actions, subject: Subjects, conditions?: Record<string, any>): boolean {
    console.log("action "+action);
    const ruleExists= this.rules.some(rule => {
      if (rule.action !== action && rule.action !== 'manage') return false;
      if (rule.subject !== subject && rule.subject !== 'all') return false;
      if (rule.conditions && conditions) {
        return Object.keys(rule.conditions).every(key => rule.conditions![key] === conditions[key]);
      }
      return true;
    });
    return ruleExists;
  }
}

export default Ability;