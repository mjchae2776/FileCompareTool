using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace File_Compare_Tool1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // 디자인 창에서 설정해도 되지만, 코드로 확실히 지정합니다.
            lvwLeftDir.OwnerDraw = true;
            lvwRightDir.OwnerDraw = true;
            lvwLeftDir.View = View.Details;
            lvwRightDir.View = View.Details;
        }

        // --- 1. 파일 목록을 가져오고 색상 상태를 결정하는 함수 ---
        private void PopulateListView(ListView lvw, string currentPath, string opponentPath)
        {
            lvw.Items.Clear();
            if (string.IsNullOrWhiteSpace(currentPath) || !Directory.Exists(currentPath)) return;

            DirectoryInfo currentDir = new DirectoryInfo(currentPath);

            foreach (FileInfo lf in currentDir.GetFiles())
            {
                // 리스트뷰 아이템 생성 (3단계: 사용자 정의 출력 준비)
                var litem = new ListViewItem(lf.Name);
                litem.SubItems.Add((lf.Length / 1024).ToString() + " KB");
                litem.SubItems.Add(lf.LastWriteTime.ToString("g"));

                // 상대방 폴더에 같은 이름의 파일이 있는지 확인
                string opponentFilePath = Path.Combine(opponentPath, lf.Name);
                FileInfo rf = File.Exists(opponentFilePath) ? new FileInfo(opponentFilePath) : null;

                // [색상 결정 로직]
                if (rf != null)
                {
                    // 1단계: 동일 파일 (검은색)
                    if (lf.LastWriteTime == rf.LastWriteTime)
                        litem.ForeColor = Color.Black;
                    // 2단계: 다른 파일 (New-빨간색, Old-회색)
                    else if (lf.LastWriteTime > rf.LastWriteTime)
                        litem.ForeColor = Color.Red;
                    else
                        litem.ForeColor = Color.Gray;
                }
                else
                {
                    // 3단계: 단독 파일 (보라색)
                    litem.ForeColor = Color.Purple;
                }

                lvw.Items.Add(litem);
            }
        }

        // --- 2. 왼쪽 폴더 선택 버튼 (창 1번만 뜨게 수정) ---
        private void btnCopyFromLeft_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "왼쪽 폴더를 선택하세요.";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtLeftDir.Text = dlg.SelectedPath;
                    // 목록 갱신 및 비교 실행
                    PopulateListView(lvwLeftDir, txtLeftDir.Text, txtRightDir.Text);
                    // 상대방(오른쪽) 리스트도 현재 선택된 왼쪽 폴더 기준으로 색상을 다시 칠해줍니다.
                    PopulateListView(lvwRightDir, txtRightDir.Text, txtLeftDir.Text);
                }
            }
        }

        // --- 3. 오른쪽 폴더 선택 버튼 (창 1번만 뜨게 수정) ---
        private void btnCopyFromRight_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "오른쪽 폴더를 선택하세요.";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtRightDir.Text = dlg.SelectedPath;
                    // 목록 갱신 및 비교 실행
                    PopulateListView(lvwRightDir, txtRightDir.Text, txtLeftDir.Text);
                    // 상대방(왼쪽) 리스트도 현재 선택된 오른쪽 폴더 기준으로 색상을 다시 칠해줍니다.
                    PopulateListView(lvwLeftDir, txtLeftDir.Text, txtRightDir.Text);
                }
            }
        }

        // --- 4. DrawItem 이벤트 (사용자 정의 색상 출력) ---
        // ※ 주의: 디자인 창에서 각 리스트뷰의 이벤트에 이 함수들을 연결해야 합니다!

        private void lvwLeftDir_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawBackground();
            // 아이템의 ForeColor를 사용하여 텍스트 출력
            TextRenderer.DrawText(e.Graphics, e.Item.Text, e.Item.Font, e.Bounds, e.Item.ForeColor, TextFormatFlags.Left);
        }

        private void lvwLeftDir_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            // 서브 아이템(크기, 수정일)도 부모 아이템의 ForeColor를 따릅니다.
            TextRenderer.DrawText(e.Graphics, e.SubItem.Text, e.SubItem.Font, e.Bounds, e.Item.ForeColor, TextFormatFlags.Left);
        }

        private void lvwLeftDir_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true; // 헤더는 시스템 기본값 사용
        }

        // 오른쪽 리스트뷰용 드로우 이벤트 (왼쪽과 동일한 함수를 연결해도 무방합니다)
        private void lvwRigDir_DrawItem(object sender, DrawListViewItemEventArgs e) => lvwLeftDir_DrawItem(sender, e);
        private void lvwRigDir_DrawSubItem(object sender, DrawListViewSubItemEventArgs e) => lvwLeftDir_DrawSubItem(sender, e);
        private void lvwRigDir_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e) => lvwLeftDir_DrawColumnHeader(sender, e);
    }
}