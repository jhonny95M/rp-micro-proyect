DROP FUNCTION IF EXISTS public.get_user_by_id(int);

CREATE OR REPLACE FUNCTION public.get_user_by_id(
        p_user_id integer
        
)
returns TABLE
(
    username character varying,
    email character varying,
    DateOfBirth date,
    Password character varying,
    roleid integer,
    IsActive boolean
)
LANGUAGE 'plpgsql' 
AS $$
BEGIN
RETURN QUERY
SELECT u.username, u.email, u.date_of_birth, u.password, u.role_id, u.is_active from public.users u where u.id = p_user_id;
END;
$$
