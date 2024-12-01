CREATE TABLE IF NOT EXISTS users
(
    id               uuid NOT NULL,
    login            character varying(30) NOT NULL,
    hashed_password  character varying(88) NOT NULL,
    salt             character varying(88) NOT NULL,

    CONSTRAINT PRIMARY KEY (id)
)