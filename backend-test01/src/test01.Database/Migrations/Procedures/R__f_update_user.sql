DROP PROCEDURE IF EXISTS public.update_user(INTEGER, VARCHAR(50), VARCHAR(100), DATE, INTEGER, BOOLEAN, timestamp with time zone);

CREATE OR REPLACE PROCEDURE public.update_user(
    p_id INTEGER,
    p_username VARCHAR(50),
    p_email VARCHAR(100),
    p_password VARCHAR(100),
    p_date_of_birth DATE,
    p_role_id INTEGER,
    p_is_active BOOLEAN,
    p_updated_at timestamp with time zone
)
LANGUAGE 'plpgsql'
AS $BODY$
DECLARE 
BEGIN
    UPDATE public.users
    SET
        username = p_username,
        email = p_email,
        password = crypt(p_password, gen_salt('bf')),
        date_of_birth = p_date_of_birth,
        role_id = p_role_id,
        is_active = p_is_active,
        updated_at = p_updated_at
    WHERE id = p_id;
END;
$BODY$;