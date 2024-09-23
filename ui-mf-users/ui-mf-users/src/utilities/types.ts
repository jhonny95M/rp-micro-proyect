// types.ts
export type Actions = 'create' | 'read' | 'update' | 'delete' | 'manage' | 'edit' | 'search';
export type Subjects = 'user' | 'article'| 'uidocument' | 'all';

export interface Rule {
  action: Actions;
  subject: Subjects;
  route: string;
  
  // conditions?: Record<string, any>;
}