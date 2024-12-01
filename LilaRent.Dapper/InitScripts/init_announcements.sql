CREATE TABLE IF NOT EXISTS public.announcements
(
    id                           uuid NOT NULL,
    profile_id                   uuid NOT NULL,
    rent_object_name             character varying(50) NOT NULL,
    description                  character varying(500),
    price_value                  numeric NOT NULL,
    price_time_unit              integer NOT NULL,
    is_promoted boolean          NOT NULL,
    open_time                    time without time zone NOT NULL,
    close_time                   time without time zone NOT NULL,
    break_between_reservation    interval NOT NULL,
    min_reservation_time         interval NOT NULL,
    max_reservation_time         interval NOT NULL,
    can_clients_change_records   boolean NOT NULL,
    can_clients_disable_records  boolean NOT NULL,
    use_discount boolean         NOT NULL,
    offer_agreement_path         textNOT NULL,
    "DiscountPercentage"         integer,
    address                      character varying(70),
    max_time_for_discount        interval,
    min_time_for_discount        interval,

    CONSTRAINT PRIMARY KEY (id),

    CONSTRAINT FOREIGN KEY (profile_id)
        REFERENCES legal_person_profiles (id)
        ON DELETE CASCADE
)
