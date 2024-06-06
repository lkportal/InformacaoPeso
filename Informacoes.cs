﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedidoPeso {
    public partial class Informacoes : Form {

        private uint IDUsuario;
        public Informacoes(uint idUsuario) {
            InitializeComponent();
            this.IDUsuario = idUsuario;
        }

        private void Informacoes_Load(object sender, EventArgs e) {
            labelID.Text = IDUsuario.ToString();    
        }
    }
}
