CREATE TABLE IF NOT EXISTS refresh_tokens
(
    user_id           uuid NOT NULL,
    value             text NOT NULL,
    expires           timestamp with time zone NOT NULL,

    CONSTRAINT PRIMARY KEY (user_id)
)