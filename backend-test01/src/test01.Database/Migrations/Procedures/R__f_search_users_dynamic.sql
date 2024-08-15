DROP FUNCTION IF EXISTS public.search_users_dynamic(character varying, character varying, integer, boolean, integer, integer, character varying);

CREATE OR REPLACE FUNCTION public.search_users_dynamic(
    p_username character varying DEFAULT NULL,
    p_email character varying DEFAULT NULL,
    p_role_id integer DEFAULT NULL,
    p_is_active boolean DEFAULT NULL,
    p_page_size integer DEFAULT 10,
    p_page_current integer DEFAULT 1,
    p_search_filter character varying DEFAULT NULL
)
RETURNS TABLE
(
    id integer,
    username character varying,
    password character varying,
    email character varying,
    dateOfBirth date,
    roleid integer,
    IsActive boolean,
    totalResults integer
)
LANGUAGE 'plpgsql'
AS $$
DECLARE 
    baseQuery text := ' FROM public.users WHERE TRUE';
    queryText text;
    countQueryText text;
    totalResults integer;
BEGIN
    -- Agregar condiciones dinámicamente
    IF p_username IS NOT NULL THEN
        baseQuery := baseQuery || ' AND username ILIKE ''%' || p_username || '%''';
    END IF;

    IF p_email IS NOT NULL THEN
        baseQuery := baseQuery || ' AND email ILIKE ''%' || p_email || '%''';
    END IF;

    IF p_role_id IS NOT NULL THEN
        baseQuery := baseQuery || ' AND role_id = ' || p_role_id;
    END IF;

    IF p_is_active IS NOT NULL THEN
        baseQuery := baseQuery || ' AND is_active = ' || p_is_active;
    END IF;

    IF p_search_filter IS NOT NULL THEN
        baseQuery := baseQuery || ' AND (username ILIKE ''%' || p_search_filter || '%'' OR email ILIKE ''%' || p_search_filter || '%'')';
    END IF;

    -- Construir la consulta de conteo total
    countQueryText := 'SELECT COUNT(*)' || baseQuery;
    EXECUTE countQueryText INTO totalResults;

    -- Construir la consulta de paginación
    queryText := 'SELECT id, username, password, email, date_of_birth, role_id, is_active, ' || totalResults || ' as totalResults' || baseQuery ||
                 ' LIMIT ' || p_page_size || ' OFFSET ' || (p_page_current - 1) * p_page_size;

    -- Ejecutar la consulta de paginación
    RETURN QUERY EXECUTE queryText;
END;
$$;
