-- Habilitar la extensión pgcrypto
CREATE EXTENSION IF NOT EXISTS pgcrypto;

DROP PROCEDURE IF EXISTS public.create_user(VARCHAR(50),     VARCHAR(255),     VARCHAR(100),     DATE,     INTEGER,     BOOLEAN,  timestamp with time zone);

CREATE OR REPLACE PROCEDURE public.create_user(
    p_username VARCHAR(50),
    p_password VARCHAR(255),
    p_email VARCHAR(100),
    p_date_of_birth DATE,
    p_role_id INTEGER,
    p_is_active BOOLEAN,
    p_created_at timestamp with time zone,
    OUT p_id_out INTEGER
)
LANGUAGE 'plpgsql'
AS $BODY$
DECLARE 
    user_request_id INT;
    encrypted_password VARCHAR(255);
BEGIN
    -- Encrypt the password
    encrypted_password := crypt(p_password, gen_salt('bf'));
    INSERT INTO public.users(
        username, password, email, date_of_birth, role_id, is_active, created_at
    )
    VALUES (
        p_username, encrypted_password, p_email, p_date_of_birth, p_role_id, p_is_active, p_created_at
    )
    RETURNING id INTO user_request_id;

    p_id_out := user_request_id;
END;
$BODY$
