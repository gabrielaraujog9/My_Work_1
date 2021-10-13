using Microsoft.Xrm.Sdk;
using My_Work_1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Work_1
{
    class Program
    {
        static void Main(string[] args)
        {


            IOrganizationService service = ConnectionFactory.GetCrmService();

            Console.WriteLine("Digite o Id da oportunidade que você deseja aplicar o desconto:");
            Guid opportunityId = new Guid(Console.ReadLine());
            Console.WriteLine();
            Oportunidade oportunidade = new Oportunidade(service, opportunityId);

            EntityCollection oportunidadesCRM = oportunidade.RetriveMultipleOpportunity();

            foreach (Entity opportunity in oportunidadesCRM.Entities)
            {
                Console.WriteLine($"Nome da oportunidade: {opportunity["name"]}");
                Console.WriteLine();
                EntityReference accountId = (EntityReference)opportunity["parentaccountid"];

                Conta conta = new Conta(service, accountId.Id);

                EntityCollection accountsCRM = conta.RetriveMultipleAccountByNivelDoCliente();

                foreach (Entity accountCRM in accountsCRM.Entities)
                {
                    string nivelNome = ((AliasedValue)accountCRM["nivel.fyi_name"]).Value.ToString();
                    double valorDesconto = double.Parse(((AliasedValue)accountCRM["nivel.fyi_valordodesconto"]).Value.ToString());

                    Console.WriteLine($"Nível -> {nivelNome} tem {valorDesconto}% de desconto");
                    Money valorDetalhado = (Money)opportunity["totallineitemamount"];
                    Console.WriteLine("Valor do desconto é igual a R$ " + ((((double)valorDetalhado.Value) * valorDesconto) / 100));
                    Console.WriteLine();
                    Console.WriteLine("Você deseja atualizar essa oportunidade? Y/N");
                    string option = Console.ReadLine().ToUpper();

                    do
                    {
                        if(option != "Y" && option != "N")
                        {
                            Console.WriteLine("Digite uma opção valida: Y ou N");
                            option = Console.ReadLine().ToUpper();
                        }
                        if (option == "Y")
                        {
                            Console.WriteLine();
                            oportunidade.UpdateOpportunity(valorDetalhado, valorDesconto);
                            Console.WriteLine("Oportunidade atualizada!");
                        }                                        
                    } while (option != "Y" && option != "N");
                }


            }
            Console.WriteLine();
            Console.WriteLine("Obrigado por utilizar o programa!");
            Console.ReadKey();
        }

    }
}
