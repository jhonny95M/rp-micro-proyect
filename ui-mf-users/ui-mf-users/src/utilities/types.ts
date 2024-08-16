// types.ts
export type Actions = 'create' | 'read' | 'update' | 'delete' | 'manage' | 'edit';
export type Subjects = 'user' | 'article' | 'all';

export interface Rule {
  action: Actions;
  subject: Subjects;
  conditions?: Record<string, any>;
}