import React, { createContext, useContext } from 'react';
import Ability from '../utilities/ability';

const AbilityContext = createContext<Ability | null>(null);

export const AbilityProvider: React.FC<{ ability: Ability, children: React.ReactNode }> = ({ ability, children }) => {
  return <AbilityContext.Provider value={ability}>{children}</AbilityContext.Provider>;
};

export const useAbility = () => {
  const context = useContext(AbilityContext);
  if (!context) {
    throw new Error('useAbility must be used within an AbilityProvider');
  }
  return context;
};