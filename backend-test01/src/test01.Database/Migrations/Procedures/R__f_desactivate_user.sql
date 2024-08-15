DROP PROCEDURE IF EXISTS public.desactivate_user(INTEGER);

CREATE OR REPLACE PROCEDURE public.desactivate_user(
    p_id INTEGER
)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN
    UPDATE public.users
    SET
        is_active = FALSE
    WHERE id = p_id;
END;
$BODY$;