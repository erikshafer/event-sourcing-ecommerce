create table if not exists catalog.product_drafts
(
    product_id varchar(256) not null primary key,
    sku        varchar(32)  not null,
    brand_name varchar(128),
    created_at timestamp
);
