using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InfoTech.Migrations
{
    public partial class One : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "brand",
                columns: table => new
                {
                    brand_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    brand_name = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    brand_year = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_brand", x => x.brand_id);
                });

            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    category_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    category_name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    category_availability = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.category_id);
                });

            migrationBuilder.CreateTable(
                name: "discount",
                columns: table => new
                {
                    discount_code = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    discount_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    discount_percentage = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__discount__75C1F00773645239", x => x.discount_code);
                });

            migrationBuilder.CreateTable(
                name: "general_address",
                columns: table => new
                {
                    address_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    country = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    region = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    city = table.Column<string>(unicode: false, maxLength: 40, nullable: false),
                    street = table.Column<string>(unicode: false, maxLength: 40, nullable: false),
                    number = table.Column<int>(nullable: false),
                    zip_code = table.Column<string>(unicode: false, maxLength: 7, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__general___CAA247C8FDFC7460", x => x.address_id);
                });

            migrationBuilder.CreateTable(
                name: "payment",
                columns: table => new
                {
                    payment_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    payment_type = table.Column<string>(unicode: false, maxLength: 4, nullable: true, defaultValueSql: "('Card')"),
                    payment_value = table.Column<decimal>(type: "numeric(12, 2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment", x => x.payment_id);
                });

            migrationBuilder.CreateTable(
                name: "store",
                columns: table => new
                {
                    store_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    store_name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    phone = table.Column<string>(unicode: false, maxLength: 15, nullable: true),
                    email = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    country = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    region = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    city = table.Column<string>(unicode: false, maxLength: 40, nullable: false),
                    street = table.Column<string>(unicode: false, maxLength: 40, nullable: false),
                    zip_code = table.Column<string>(unicode: false, maxLength: 7, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_store", x => x.store_id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    product_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    product_name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    brand_id = table.Column<int>(nullable: false),
                    description = table.Column<string>(unicode: false, maxLength: 150, nullable: false),
                    product_price = table.Column<decimal>(type: "numeric(9, 2)", nullable: true),
                    product_image = table.Column<string>(nullable: true),
                    origin = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    in_stock = table.Column<bool>(nullable: true),
                    category_id = table.Column<int>(nullable: false),
                    product_discount_percent = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.product_id);
                    table.ForeignKey(
                        name: "FK__product__brand_i__59FA5E80",
                        column: x => x.brand_id,
                        principalTable: "brand",
                        principalColumn: "brand_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__product__categor__5AEE82B9",
                        column: x => x.category_id,
                        principalTable: "category",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    customer_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    first_name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    last_name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    phone = table.Column<string>(unicode: false, maxLength: 15, nullable: true),
                    email = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    address_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.customer_id);
                    table.ForeignKey(
                        name: "FK__customer__addres__68487DD7",
                        column: x => x.address_id,
                        principalTable: "general_address",
                        principalColumn: "address_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "delivery_address",
                columns: table => new
                {
                    delivery_address_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    first_name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    last_name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    phone = table.Column<string>(unicode: false, maxLength: 15, nullable: true),
                    email = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    address_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_delivery_address", x => x.delivery_address_id);
                    table.ForeignKey(
                        name: "FK__delivery___addre__4BAC3F29",
                        column: x => x.address_id,
                        principalTable: "general_address",
                        principalColumn: "address_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "product_category",
                columns: table => new
                {
                    product_id = table.Column<int>(nullable: false),
                    category_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__product___1A56936E94F76A3D", x => new { x.product_id, x.category_id });
                    table.ForeignKey(
                        name: "FK__product_c__categ__656C112C",
                        column: x => x.category_id,
                        principalTable: "category",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__product_c__produ__6477ECF3",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "stock",
                columns: table => new
                {
                    product_id = table.Column<int>(nullable: false),
                    store_id = table.Column<int>(nullable: false),
                    quantity = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__stock__9D2D57C5EAA0285C", x => new { x.product_id, x.store_id });
                    table.ForeignKey(
                        name: "FK__stock__product_i__60A75C0F",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__stock__store_id__619B8048",
                        column: x => x.store_id,
                        principalTable: "store",
                        principalColumn: "store_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    order_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    customer_id = table.Column<int>(nullable: false),
                    order_date = table.Column<DateTime>(type: "date", nullable: true),
                    delivery_address_id = table.Column<int>(nullable: false),
                    store_id = table.Column<int>(nullable: false),
                    discount_code = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    payment_id = table.Column<int>(nullable: false),
                    order_status = table.Column<string>(unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__order__0A8F3E919260B283", x => new { x.order_id, x.customer_id });
                    table.UniqueConstraint("AK_order_order_id", x => x.order_id);
                    table.ForeignKey(
                        name: "FK__order__customer___6C190EBB",
                        column: x => x.customer_id,
                        principalTable: "customer",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__order__delivery___6D0D32F4",
                        column: x => x.delivery_address_id,
                        principalTable: "delivery_address",
                        principalColumn: "delivery_address_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__order__discount___6EF57B66",
                        column: x => x.discount_code,
                        principalTable: "discount",
                        principalColumn: "discount_code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__order__payment_i__6FE99F9F",
                        column: x => x.payment_id,
                        principalTable: "payment",
                        principalColumn: "payment_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__order__store_id__6E01572D",
                        column: x => x.store_id,
                        principalTable: "store",
                        principalColumn: "store_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "order_product",
                columns: table => new
                {
                    order_id = table.Column<int>(nullable: false),
                    product_id = table.Column<int>(nullable: false),
                    quantity = table.Column<int>(nullable: false),
                    price = table.Column<decimal>(type: "numeric(9, 2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__order_pr__022945F640295809", x => new { x.order_id, x.product_id });
                    table.ForeignKey(
                        name: "FK__order_pro__order__73BA3083",
                        column: x => x.order_id,
                        principalTable: "order",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__order_pro__produ__74AE54BC",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_customer_address_id",
                table: "customer",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "IX_delivery_address_address_id",
                table: "delivery_address",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "UQ__discount__BDBE9EF828A6921A",
                table: "discount",
                column: "discount_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_order_customer_id",
                table: "order",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_delivery_address_id",
                table: "order",
                column: "delivery_address_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_discount_code",
                table: "order",
                column: "discount_code");

            migrationBuilder.CreateIndex(
                name: "UQ__order__4659622899651CE0",
                table: "order",
                column: "order_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_order_payment_id",
                table: "order",
                column: "payment_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_store_id",
                table: "order",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_product_product_id",
                table: "order_product",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_brand_id",
                table: "product",
                column: "brand_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_category_id",
                table: "product",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_category_category_id",
                table: "product_category",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_stock_store_id",
                table: "stock",
                column: "store_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "order_product");

            migrationBuilder.DropTable(
                name: "product_category");

            migrationBuilder.DropTable(
                name: "stock");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "delivery_address");

            migrationBuilder.DropTable(
                name: "discount");

            migrationBuilder.DropTable(
                name: "payment");

            migrationBuilder.DropTable(
                name: "store");

            migrationBuilder.DropTable(
                name: "brand");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "general_address");
        }
    }
}
