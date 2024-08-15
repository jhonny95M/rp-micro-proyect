
ALTER TABLE public."users"
RENAME COLUMN "CreateUser" TO create_user;
ALTER TABLE public."users"
RENAME COLUMN "UpdateUser" TO update_user;