﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SalesProject.Infra.Context;

namespace SalesProject.Infra.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210819011332_BugFix")]
    partial class BugFix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SalesProject.Domain.Entities.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Id gerado pela aplicação.");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("Cidade")
                        .HasComment("Cidade do endereço.");

                    b.Property<string>("CodeCity")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("CodigoIbgeCidade")
                        .HasComment("Código IBGE da cidade. Usado para emissão de NF.");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ClienteId")
                        .HasComment("Vínculo com a tabela de Cliente (Fk).");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Descricao")
                        .HasComment("Descrição do endereço.");

                    b.Property<string>("Neighborhood")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("Bairro")
                        .HasComment("Bairro do endereço.");

                    b.Property<int>("Number")
                        .HasColumnType("int")
                        .HasColumnName("Numero")
                        .HasComment("Número do endereço.");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)")
                        .HasColumnName("Uf")
                        .HasComment("Estado do endereço.");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("varchar(120)")
                        .HasColumnName("Logradouro")
                        .HasComment("Logradouro do endereço.");

                    b.Property<int?>("Type")
                        .HasColumnType("int")
                        .HasColumnName("Tipo")
                        .HasComment("Tipo do endereço. 1: Cobrança, 2: Entrega, 3: Outro.");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasMaxLength(18)
                        .HasColumnType("varchar(18)")
                        .HasColumnName("Cep")
                        .HasComment("Código postal do endereço.");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Endereco");
                });

            modelBuilder.Entity("SalesProject.Domain.Entities.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Id gerado pela aplicação.");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ClienteId")
                        .HasComment("Vínculo com a tabela de Cliente (Fk).");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Email")
                        .HasComment("E-mail do contato.");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("Nome")
                        .HasComment("Primeiro nome do contato.");

                    b.Property<string>("FullName")
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("NomeSobrenome")
                        .HasComment("Primeiro nome e último nome");

                    b.Property<string>("LastName")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("Sobrenome")
                        .HasComment("Último nome do contato.");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("Telefone")
                        .HasComment("Telefone do contato.");

                    b.Property<string>("WhatsApp")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("WhatsApp")
                        .HasComment("Número do WhatsApp do contato.");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Contato");
                });

            modelBuilder.Entity("SalesProject.Domain.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Id gerado pela aplicação.");

                    b.Property<DateTime>("ClientSince")
                        .HasColumnType("date")
                        .HasColumnName("DataCadastro")
                        .HasComment("Data de cadastrado do cliente no sistema.");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasMaxLength(18)
                        .HasColumnType("varchar(18)")
                        .HasComment("Número do cadastro nacional de pessoa jurídica do cliente.");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Empresa")
                        .HasComment("Nome da empresa do cliente.");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Email")
                        .HasComment("E-mail da empresa do cliente");

                    b.Property<string>("MunicipalRegistration")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("InscricaoMunicipal")
                        .HasComment("Inscrição municipal da empresa do cliente.");

                    b.Property<DateTime?>("Opening")
                        .HasColumnType("date")
                        .HasColumnName("DataAbertura")
                        .HasComment("Data de abertura da empresa do cliente.");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("Telefone")
                        .HasComment("Telefone da empresa do cliente.");

                    b.Property<string>("StateRegistration")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("InscricaoEstadual")
                        .HasComment("Inscrição estadual da empresa do cliente.");

                    b.HasKey("Id");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("SalesProject.Domain.Entities.Invoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Id gerado pela aplicação.");

                    b.Property<decimal>("BaseCalcIcms")
                        .HasColumnType("money")
                        .HasColumnName("BaseCalculoIcms")
                        .HasComment("Base para Calculo do Icms.");

                    b.Property<string>("IdPlugNotasIntegration")
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("NotaIdPlugNotas")
                        .HasComment("Chave da Nota Fiscal integrada com Plug Notas Api.");

                    b.Property<string>("IntegratedPlugNotasApi")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("varchar(1)")
                        .HasColumnName("NotaEnviada")
                        .HasComment("Nota fiscal foi enviada para Sefaz? Y: Sim, N: Não.");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("PedidoId")
                        .HasComment("Vínculo com a tabela de Pedido (Fk).");

                    b.Property<string>("OriginOperation")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)")
                        .HasColumnName("NaturezaOperacao")
                        .HasComment("Natureza de Operação da Nota fiscal.");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("date")
                        .HasColumnName("DataEmissao")
                        .HasComment("Data de Emissão da Nota fiscal.");

                    b.Property<decimal>("TotalIcms")
                        .HasColumnType("money")
                        .HasColumnName("ValorIcms")
                        .HasComment("Valor total do Icms na Nota fiscal.");

                    b.Property<decimal>("TotalInvoice")
                        .HasColumnType("money")
                        .HasColumnName("TotalNota")
                        .HasComment("Valor total da Nota fiscal.");

                    b.Property<decimal>("TotalProducts")
                        .HasColumnType("money")
                        .HasColumnName("TotalProdutos")
                        .HasComment("Valor total dos produtos da Nota fiscal.");

                    b.HasKey("Id");

                    b.HasIndex("OrderId")
                        .IsUnique()
                        .HasFilter("[PedidoId] IS NOT NULL");

                    b.ToTable("NotaFiscal");
                });

            modelBuilder.Entity("SalesProject.Domain.Entities.InvoiceLine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Id gerado pela aplicação.");

                    b.Property<decimal>("AdditionalCosts")
                        .HasColumnType("money")
                        .HasColumnName("CustosAdicionais")
                        .HasComment("Custos adicionais do item da Nota fiscal.");

                    b.Property<double>("AliquotCofins")
                        .HasColumnType("float")
                        .HasColumnName("AliquotaCofins")
                        .HasComment("Alíquota do Cofins do item da Nota fiscal.");

                    b.Property<double>("AliquotIcms")
                        .HasColumnType("float")
                        .HasColumnName("AliquotaIcms")
                        .HasComment("Alíquota do Icms do item da Nota fiscal.");

                    b.Property<double>("AliquotPis")
                        .HasColumnType("float")
                        .HasColumnName("AliquotaPis")
                        .HasComment("Alíquota do Pis do item da Nota fiscal.");

                    b.Property<string>("CstCofins")
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)")
                        .HasColumnName("CstCofins")
                        .HasComment("Cst Cofins do item da Nota fiscal.");

                    b.Property<string>("CstIcms")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)")
                        .HasColumnName("CstIcms")
                        .HasComment("Cst Icms do item da Nota fiscal.");

                    b.Property<string>("CstPis")
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)")
                        .HasColumnName("CstPis")
                        .HasComment("Cst Pis do item da Nota fiscal.");

                    b.Property<int>("DeterminationMode")
                        .HasColumnType("int")
                        .HasColumnName("ModalidadeDeterminacao")
                        .HasComment("Modalidade Determinacao do item da Nota fiscal.");

                    b.Property<Guid>("InvoiceId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("NotaFiscalId")
                        .HasComment("Vínculo com a tabela de Nota fiscal (Fk).");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("NomeProduto")
                        .HasComment("Nome do produto do item da Nota fiscal.");

                    b.Property<string>("NcmCode")
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("CodigoNcm")
                        .HasComment("Código Ncm do item da Nota fiscal.");

                    b.Property<string>("OriginIcms")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)")
                        .HasColumnName("OrigemIcms")
                        .HasComment("Origem Icms do item da Nota fiscal.");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("Quantidade")
                        .HasComment("Quantidade do item da Nota fiscal.");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("money")
                        .HasColumnName("ValorTotal")
                        .HasComment("Valor total do item da Nota fiscal.");

                    b.Property<decimal>("TotalTax")
                        .HasColumnType("money")
                        .HasColumnName("ImpostoTotal")
                        .HasComment("Imposto total do item da Nota fiscal.");

                    b.Property<decimal>("UnitaryPrice")
                        .HasColumnType("money")
                        .HasColumnName("ValorUnitario")
                        .HasComment("Valor Unitário do item da Nota fiscal.");

                    b.Property<decimal>("ValueBaseCalcCofins")
                        .HasColumnType("money")
                        .HasColumnName("ValorBaseCalcCofins")
                        .HasComment("Valor Base Cálculo do Cofins do item da Nota fiscal.");

                    b.Property<decimal>("ValueBaseCalcIcms")
                        .HasColumnType("money")
                        .HasColumnName("BaseCalcIcms")
                        .HasComment("Base Cálculo do Icms do item da Nota fiscal.");

                    b.Property<decimal>("ValueBaseCalcPis")
                        .HasColumnType("money")
                        .HasColumnName("BaseCalcPis")
                        .HasComment("Base Cálculo do Pis do item da Nota fiscal.");

                    b.Property<decimal>("ValueCofins")
                        .HasColumnType("money")
                        .HasColumnName("ValorCofins")
                        .HasComment("Valor do Cofins do item da Nota fiscal.");

                    b.Property<decimal>("ValueIcms")
                        .HasColumnType("money")
                        .HasColumnName("ValorIcms")
                        .HasComment("Valor do Icms do item da Nota fiscal.");

                    b.Property<decimal>("ValuePis")
                        .HasColumnType("money")
                        .HasColumnName("ValorPis")
                        .HasComment("Valor do Pis do item da Nota fiscal.");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.ToTable("ItemNotaFiscal");
                });

            modelBuilder.Entity("SalesProject.Domain.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Id gerado pela aplicação.");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ClienteId")
                        .HasComment("Vínculo com a tabela de Cliente (Fk).");

                    b.Property<DateTime>("DeliveryDate")
                        .HasColumnType("date")
                        .HasColumnName("DataEntrega")
                        .HasComment("Data de Entrega do Pedido.");

                    b.Property<string>("Observation")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)")
                        .HasColumnName("Observacao")
                        .HasComment("Observações para o Pedido.");

                    b.Property<DateTime>("PostingDate")
                        .HasColumnType("date")
                        .HasColumnName("DataLancamento")
                        .HasComment("Data de Lançamento do Pedido.");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("Status")
                        .HasComment("Status do Pedido. 1:Aberto, 2:Aprovado, 3:Faturado, 4:Cancelado.");

                    b.Property<decimal>("TotalOrder")
                        .HasColumnType("money")
                        .HasColumnName("Total")
                        .HasComment("Total do Pedido.");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("Status", "PostingDate");

                    b.ToTable("Pedido");
                });

            modelBuilder.Entity("SalesProject.Domain.Entities.OrderLine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Id gerado pela aplicação.");

                    b.Property<decimal>("AdditionalCosts")
                        .HasColumnType("money")
                        .HasColumnName("CustosAdicionais")
                        .HasComment("Custos adicionais do item do Pedido.");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("PedidoId")
                        .HasComment("Vínculo com a tabela de Pedido (Fk).");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ProdutoId")
                        .HasComment("Vínculo com a tabela de Produto (Fk).");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("Quantidade")
                        .HasComment("Quantidade do item do Pedido.");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("money")
                        .HasColumnName("TotalItem")
                        .HasComment("Total do item do Pedido.");

                    b.Property<decimal>("UnitaryPrice")
                        .HasColumnType("money")
                        .HasColumnName("ValorUnitario")
                        .HasComment("Valor unitário do item do Pedido.");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("ItemPedido");
                });

            modelBuilder.Entity("SalesProject.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Id gerado pela aplicação.");

                    b.Property<decimal>("AdditionalCosts")
                        .HasColumnType("money")
                        .HasColumnName("CustosAdicionais")
                        .HasComment("Custos adicionais combinado para o Produto.");

                    b.Property<decimal>("CombinedPrice")
                        .HasColumnType("money")
                        .HasColumnName("PrecoCombinado")
                        .HasComment("Preço combinado do Produto.");

                    b.Property<int>("CombinedQuantity")
                        .HasColumnType("int")
                        .HasColumnName("PrevisaoMensal")
                        .HasComment("Previsão mensal mínima combinada para o Produto.");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ClienteId")
                        .HasComment("Vínculo com a tabela de Cliente (Fk).");

                    b.Property<string>("Details")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("Detalhes")
                        .HasComment("Detalhes do Produto.");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Nome")
                        .HasComment("Nome do Produto.");

                    b.Property<string>("NcmCode")
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("CodigoNcm")
                        .HasComment("Código NCM do Produto.");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Produto");
                });

            modelBuilder.Entity("SalesProject.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("Id gerado pela aplicação.");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ClienteId")
                        .HasComment("Vínculo com a tabela de Cliente (Fk).");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Email")
                        .HasComment("E-mail do usuário");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Nome")
                        .HasComment("Nome do usuário.");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)")
                        .HasColumnName("SenhaCriptografada")
                        .HasComment("Senha criptografada do usuário");

                    b.Property<int>("Role")
                        .HasColumnType("int")
                        .HasColumnName("Funcao")
                        .HasComment("Função do usuário. Atribui as permissões do usuário no sistema. 1:Cliente, 2:Vendedor, 3:Funcinário de TI, 4:Gestor.");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Usuario")
                        .HasComment("Nome de usuário para realizar login no sistema.");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .IsUnique()
                        .HasFilter("[ClienteId] IS NOT NULL");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("SalesProject.Domain.Entities.Address", b =>
                {
                    b.HasOne("SalesProject.Domain.Entities.Customer", null)
                        .WithMany("Adresses")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SalesProject.Domain.Entities.Contact", b =>
                {
                    b.HasOne("SalesProject.Domain.Entities.Customer", null)
                        .WithMany("Contacts")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SalesProject.Domain.Entities.Invoice", b =>
                {
                    b.HasOne("SalesProject.Domain.Entities.Order", "Order")
                        .WithOne()
                        .HasForeignKey("SalesProject.Domain.Entities.Invoice", "OrderId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Order");
                });

            modelBuilder.Entity("SalesProject.Domain.Entities.InvoiceLine", b =>
                {
                    b.HasOne("SalesProject.Domain.Entities.Invoice", null)
                        .WithMany("InvoiceLines")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SalesProject.Domain.Entities.Order", b =>
                {
                    b.HasOne("SalesProject.Domain.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("SalesProject.Domain.Entities.OrderLine", b =>
                {
                    b.HasOne("SalesProject.Domain.Entities.Order", null)
                        .WithMany("OrderLines")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SalesProject.Domain.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Product");
                });

            modelBuilder.Entity("SalesProject.Domain.Entities.Product", b =>
                {
                    b.HasOne("SalesProject.Domain.Entities.Customer", null)
                        .WithMany("Products")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("SalesProject.Domain.Entities.User", b =>
                {
                    b.HasOne("SalesProject.Domain.Entities.Customer", null)
                        .WithOne("User")
                        .HasForeignKey("SalesProject.Domain.Entities.User", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SalesProject.Domain.Entities.Customer", b =>
                {
                    b.Navigation("Adresses");

                    b.Navigation("Contacts");

                    b.Navigation("Products");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SalesProject.Domain.Entities.Invoice", b =>
                {
                    b.Navigation("InvoiceLines");
                });

            modelBuilder.Entity("SalesProject.Domain.Entities.Order", b =>
                {
                    b.Navigation("OrderLines");
                });
#pragma warning restore 612, 618
        }
    }
}
