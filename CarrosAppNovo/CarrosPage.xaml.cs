using CarrosAppNovo.Models;
using System;
using System.Linq;
using Microsoft.Maui.Controls;

namespace CarrosAppNovo
{
    public partial class CarrosPage : ContentPage
    {
        private readonly Conecta banco = new();
        private readonly Carro carro = new();
        private Carro? carroSelecionado;

        public CarrosPage()
        {
            InitializeComponent();
            AtualizarLista();
            if (banco.Conexao())
            {
                lblStatus.Text = "Banco conectado com Sucesso!";
            }
            else
            {
                lblStatus.Text = banco.conexao_status ?? "Status de conexao indisponível.";
            }
        }

        private void AtualizarLista(string busca = "")
        {
            if (carro.ListarCarros(busca))
            {
                lstCarros.ItemsSource = null;
                lstCarros.ItemsSource = carro.listaCarros;
            }
        }

        private void OnAdicionarClicked(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            carro.Marca = txtMarca.Text;
            carro.Modelo = txtModelo.Text;
            carro.Ano = int.Parse(txtAno.Text); 
            carro.Cor = txtCor.Text;
            carro.Placa = txtPlaca.Text;

            if (carro.Salvar(true))
            {
                DisplayAlert("Sucesso", "Carro adicionado!", "OK");
                LimparCampos();
                AtualizarLista();
            }
            else
            {
                DisplayAlert("Erro", "Nao foi possivel adicionar o carro.", "OK");
            }
        }

        private void OnAtualizarClicked(object sender, EventArgs e)
        {
            if (carroSelecionado == null)
            {
                DisplayAlert("Atencao", "Selecione um carro da lista para atualizar.", "OK");
                return;
            }

            if (!ValidarCampos()) return;

            // Atribui os novos valores ao objeto j� selecionado
            carroSelecionado.Marca = txtMarca.Text;
            carroSelecionado.Modelo = txtModelo.Text;
            carroSelecionado.Ano = int.Parse(txtAno.Text);
            carroSelecionado.Cor = txtCor.Text;
            carroSelecionado.Placa = txtPlaca.Text;

            if (carroSelecionado.Salvar())
            {
                DisplayAlert("Sucesso", "Carro atualizado!", "OK");
                LimparCampos();
                AtualizarLista();
            }
            else
            {
                DisplayAlert("Erro", "Nao foi possivel atualizar o carro.", "OK");
            }
        }

        private async void OnCarroSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;

            carroSelecionado = (Carro)e.SelectedItem;

           
            txtMarca.Text = carroSelecionado.Marca;
            txtModelo.Text = carroSelecionado.Modelo;
            txtAno.Text = carroSelecionado.Ano.ToString();
            txtCor.Text = carroSelecionado.Cor;
            txtPlaca.Text = carroSelecionado.Placa;


            ((ListView)sender).SelectedItem = null;
        }

        private void OnLimparClicked(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void LimparCampos()
        {
            txtMarca.Text = string.Empty;
            txtModelo.Text = string.Empty;
            txtAno.Text = string.Empty;
            txtCor.Text = string.Empty;
            txtPlaca.Text = string.Empty;
            carroSelecionado = null;
            lstCarros.SelectedItem = null;
        }

        private async void OnApagarClicked(object sender, EventArgs e)
        {
            
            var button = sender as Button;
            
            var carroParaApagar = button?.CommandParameter as Carro;

            if (carroParaApagar != null)
            {
                
                bool confirm = await DisplayAlert("Excluir", $"Deseja realmente excluir o carro {carroParaApagar.Modelo}?", "Sim", "Não");
                if (confirm)
                {
                    
                    if (carroParaApagar.Excluir())
                    {
                        await DisplayAlert("Sucesso", "Carro excluído!", "OK");
                        LimparCampos(); 
                        AtualizarLista(); 
                    }
                    else
                    {
                        await DisplayAlert("Erro", "Não foi possível excluir o carro.", "OK");
                    }
                }
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtMarca.Text) ||
                string.IsNullOrWhiteSpace(txtModelo.Text) ||
                string.IsNullOrWhiteSpace(txtAno.Text) ||
                string.IsNullOrWhiteSpace(txtCor.Text) ||
                string.IsNullOrWhiteSpace(txtPlaca.Text))
            {
                DisplayAlert("Atencaoo", "Preencha todos os campos.", "OK");
                return false;
            }
            if (!int.TryParse(txtAno.Text, out _))
            {
                DisplayAlert("Atencaoo", "O ano deve ser um numero valido.", "OK");
                return false;
            }
            return true;
        }

        private void OnSearchButtonPressed(object sender, EventArgs e)
        {
            AtualizarLista(searchBar.Text);
        }
    }
}