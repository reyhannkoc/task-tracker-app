using System;
using System.Windows.Forms;
using BusinessLayer;
using DataAccessLayer;
using EntityLayer;

namespace PresentationLayer
{
    public partial class Form1 : Form
    {
        private readonly TaskDbContext _context;
        private readonly TaskManager _taskManager;

        public Form1()
        {
            InitializeComponent();
            _context = new TaskDbContext();
            _taskManager = new TaskManager(_context);
            Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbCategory.Items.Clear();
            cmbCategory.Items.Add("General");
            cmbCategory.Items.Add("Work");
            cmbCategory.Items.Add("School");
            cmbCategory.Items.Add("Personal");
            cmbCategory.SelectedIndex = 0;

            dtpDueDate.Value = DateTime.Now.AddDays(1);
            LoadAllTasks();
        }

        private void LoadAllTasks()
        {
            dgvTasks.DataSource = null;
            dgvTasks.DataSource = _taskManager.GetAllTasks();
        }

        private int GetSelectedTaskId()
        {
            if (dgvTasks.CurrentRow == null)
            {
                return 0;
            }

            return Convert.ToInt32(dgvTasks.CurrentRow.Cells["Id"].Value);
        }

        private void btnAddTask_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Please enter a task title.");
                return;
            }

            var taskItem = new TaskItem
            {
                Title = txtTitle.Text.Trim(),
                Category = cmbCategory.Text,
                DueDate = dtpDueDate.Value.Date,
                IsCompleted = false
            };

            _taskManager.AddTask(taskItem);
            LoadAllTasks();
            txtTitle.Clear();
            cmbCategory.SelectedIndex = 0;
            dtpDueDate.Value = DateTime.Now.AddDays(1);
        }

        private void btnDeleteTask_Click(object sender, EventArgs e)
        {
            var taskId = GetSelectedTaskId();
            if (taskId == 0)
            {
                MessageBox.Show("Please select a task to delete.");
                return;
            }

            _taskManager.DeleteTask(taskId);
            LoadAllTasks();
        }

        private void btnMarkCompleted_Click(object sender, EventArgs e)
        {
            var taskId = GetSelectedTaskId();
            if (taskId == 0)
            {
                MessageBox.Show("Please select a task to mark as completed.");
                return;
            }

            _taskManager.MarkAsCompleted(taskId);
            LoadAllTasks();
        }

        private void btnListAll_Click(object sender, EventArgs e)
        {
            LoadAllTasks();
        }

        private void btnListCompleted_Click(object sender, EventArgs e)
        {
            dgvTasks.DataSource = null;
            dgvTasks.DataSource = _taskManager.GetCompletedTasks();
        }

        private void btnListPending_Click(object sender, EventArgs e)
        {
            dgvTasks.DataSource = null;
            dgvTasks.DataSource = _taskManager.GetPendingTasks();
        }
    }
}
