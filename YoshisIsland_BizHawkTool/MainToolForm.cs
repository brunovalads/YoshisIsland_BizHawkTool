using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace YoshisIsland_BizHawkTool
{
    [ExternalTool("Yoshi's Island Utilities", Description = "Yoshi's Island (SNES) Utilities for BizHawk 2.10+")] // this appears in the Tools > External Tools submenu in EmuHawk
    [ExternalToolEmbeddedIcon("YoshisIsland_BizHawkTool.Resources.Images.yoshi_icon_16px.png")]
    public class MainToolForm : ToolFormBase, IExternalToolForm
    {
        // CONSTANTS ==========
        public const string TOOL_NAME = "YoshisIsland_BizHawkTool";
        public const string WINDOW_TITLE = "Yoshi's Island Utilities Tool";


        // FIELDS ==========
        // UI
        private FlowLayoutPanel utilitiesFlowLayoutPanel;
        private GroupBox playerGroupBox;
        private CheckBox playerInfoCheckBox;
        private CheckBox playerHitboxCheckBox;
        private CheckBox playerSolidInteractionCheckBox;
        private CheckBox playerBlockedStatusCheckBox;
        private CheckBox playerThrowInfoCheckBox;
        private CheckBox playerEggInventoryCheckBox;
        private CheckBox playerTongueHitboxCheckBox;
        private GroupBox spritesGroupBox;
        private CheckBox spritesInfoCheckBox;
        private CheckBox spritesTableCheckBox;
        private CheckBox spritesHitboxCheckBox;
        private CheckBox spritesSpecialInfoCheckBox;
        private CheckBox spritesSpawningAreasCheckBox;
        private Button spriteTablesButton;
        private GroupBox levelGroupBox;
        private CheckBox levelInfoCheckBox;
        private CheckBox levelSpriteDataCheckBox;
        private CheckBox levelExtraCheckBox;
        private CheckBox levelTileGridCheckBox;
        private CheckBox levelTileTypesCheckBox;
        private CheckBox levelScreensCheckBox;
        private CheckBox levelLayoutCheckBox;
        private GroupBox utilitiesGroupBox;
        private GroupBox settingsGroupBox;
        private CheckBox mouseInfoCheckBox;
        private CheckBox drawTilesCheckBox;
        private NumericUpDown leftGapNumericUpDown;
        private NumericUpDown filterNumericUpDown;
        private CheckBox darkFilterCheckBox;
        private Button eraseTilesButton;
        private Label gapsLabel;
        private NumericUpDown bottomGapNumericUpDown;
        private NumericUpDown rightGapNumericUpDown;
        private NumericUpDown topGapNumericUpDown;
        private Button levelMapButton;
        private GroupBox generalGroupBox;
        private CheckBox creditsWarpHelperCheckBox;
        private CheckBox movieInfoCheckBox;
        private CheckBox countersCheckBox;
        private CheckBox miscInfoCheckBox;
        private CheckBox overworldInfoCheckBox;
        private GroupBox ambientSpritesGroupBox;
        private CheckBox ambientSpritesSlotInScreenCheckBox;
        private CheckBox ambientSpritesTableCheckBox;
        private CheckBox ambientSpritesInfoCheckBox;
        private GroupBox debugGroupBox;
        private CheckBox lagmeterCheckBox;
        private CheckBox debugControllerDataCheckBox;
        private CheckBox spriteLoadStatusCheckBox;
        private CheckBox debugSpriteExtraCheckBox;
        private CheckBox debugPlayerExtraCheckBox;

        //Other
        private ToolOptions _toolOptions = ToolOptions.Instance;


        // PROPERTIES ==========
        protected override string WindowTitleStatic => WINDOW_TITLE;
        public ApiContainer? _maybeAPIContainer { get; set; }
        private ApiContainer APIs => _maybeAPIContainer!;


        // CONSTRUCTOR ==========
        public MainToolForm()
        {
            InitializeComponent();
            SetBindings();

        }


        // OVERRIDE METHODS ==========
        public override void Restart()
        {
            base.Restart();
        }

        protected override void UpdateAfter()
        {
            APIs.Gui.ClearGraphics(DisplaySurfaceID.EmuCore);
            APIs.Gui.ClearGraphics(DisplaySurfaceID.Client);

#if DEBUG
            _toolOptions.Debug(APIs.Gui);
#endif
        }


        // PRIVATE METHODS ==========
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainToolForm));
            this.utilitiesFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.utilitiesGroupBox = new System.Windows.Forms.GroupBox();
            this.playerGroupBox = new System.Windows.Forms.GroupBox();
            this.playerTongueHitboxCheckBox = new System.Windows.Forms.CheckBox();
            this.playerEggInventoryCheckBox = new System.Windows.Forms.CheckBox();
            this.playerThrowInfoCheckBox = new System.Windows.Forms.CheckBox();
            this.playerBlockedStatusCheckBox = new System.Windows.Forms.CheckBox();
            this.playerSolidInteractionCheckBox = new System.Windows.Forms.CheckBox();
            this.playerHitboxCheckBox = new System.Windows.Forms.CheckBox();
            this.playerInfoCheckBox = new System.Windows.Forms.CheckBox();
            this.levelGroupBox = new System.Windows.Forms.GroupBox();
            this.levelLayoutCheckBox = new System.Windows.Forms.CheckBox();
            this.levelScreensCheckBox = new System.Windows.Forms.CheckBox();
            this.levelMapButton = new System.Windows.Forms.Button();
            this.levelTileTypesCheckBox = new System.Windows.Forms.CheckBox();
            this.levelTileGridCheckBox = new System.Windows.Forms.CheckBox();
            this.levelSpriteDataCheckBox = new System.Windows.Forms.CheckBox();
            this.levelExtraCheckBox = new System.Windows.Forms.CheckBox();
            this.levelInfoCheckBox = new System.Windows.Forms.CheckBox();
            this.spritesGroupBox = new System.Windows.Forms.GroupBox();
            this.spriteTablesButton = new System.Windows.Forms.Button();
            this.spritesSpawningAreasCheckBox = new System.Windows.Forms.CheckBox();
            this.spritesSpecialInfoCheckBox = new System.Windows.Forms.CheckBox();
            this.spritesTableCheckBox = new System.Windows.Forms.CheckBox();
            this.spritesHitboxCheckBox = new System.Windows.Forms.CheckBox();
            this.spritesInfoCheckBox = new System.Windows.Forms.CheckBox();
            this.settingsGroupBox = new System.Windows.Forms.GroupBox();
            this.gapsLabel = new System.Windows.Forms.Label();
            this.bottomGapNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.rightGapNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.topGapNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.leftGapNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.filterNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.darkFilterCheckBox = new System.Windows.Forms.CheckBox();
            this.eraseTilesButton = new System.Windows.Forms.Button();
            this.mouseInfoCheckBox = new System.Windows.Forms.CheckBox();
            this.drawTilesCheckBox = new System.Windows.Forms.CheckBox();
            this.generalGroupBox = new System.Windows.Forms.GroupBox();
            this.creditsWarpHelperCheckBox = new System.Windows.Forms.CheckBox();
            this.movieInfoCheckBox = new System.Windows.Forms.CheckBox();
            this.countersCheckBox = new System.Windows.Forms.CheckBox();
            this.miscInfoCheckBox = new System.Windows.Forms.CheckBox();
            this.overworldInfoCheckBox = new System.Windows.Forms.CheckBox();
            this.ambientSpritesGroupBox = new System.Windows.Forms.GroupBox();
            this.ambientSpritesSlotInScreenCheckBox = new System.Windows.Forms.CheckBox();
            this.ambientSpritesTableCheckBox = new System.Windows.Forms.CheckBox();
            this.ambientSpritesInfoCheckBox = new System.Windows.Forms.CheckBox();
            this.debugGroupBox = new System.Windows.Forms.GroupBox();
            this.lagmeterCheckBox = new System.Windows.Forms.CheckBox();
            this.debugControllerDataCheckBox = new System.Windows.Forms.CheckBox();
            this.spriteLoadStatusCheckBox = new System.Windows.Forms.CheckBox();
            this.debugSpriteExtraCheckBox = new System.Windows.Forms.CheckBox();
            this.debugPlayerExtraCheckBox = new System.Windows.Forms.CheckBox();
            this.utilitiesFlowLayoutPanel.SuspendLayout();
            this.utilitiesGroupBox.SuspendLayout();
            this.playerGroupBox.SuspendLayout();
            this.levelGroupBox.SuspendLayout();
            this.spritesGroupBox.SuspendLayout();
            this.settingsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bottomGapNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightGapNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.topGapNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftGapNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterNumericUpDown)).BeginInit();
            this.generalGroupBox.SuspendLayout();
            this.ambientSpritesGroupBox.SuspendLayout();
            this.debugGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // utilitiesFlowLayoutPanel
            // 
            this.utilitiesFlowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.utilitiesFlowLayoutPanel.Controls.Add(this.utilitiesGroupBox);
            this.utilitiesFlowLayoutPanel.Controls.Add(this.settingsGroupBox);
            this.utilitiesFlowLayoutPanel.Location = new System.Drawing.Point(12, 12);
            this.utilitiesFlowLayoutPanel.Name = "utilitiesFlowLayoutPanel";
            this.utilitiesFlowLayoutPanel.Size = new System.Drawing.Size(386, 529);
            this.utilitiesFlowLayoutPanel.TabIndex = 1;
            // 
            // utilitiesGroupBox
            // 
            this.utilitiesGroupBox.AutoSize = true;
            this.utilitiesGroupBox.Controls.Add(this.debugGroupBox);
            this.utilitiesGroupBox.Controls.Add(this.ambientSpritesGroupBox);
            this.utilitiesGroupBox.Controls.Add(this.generalGroupBox);
            this.utilitiesGroupBox.Controls.Add(this.playerGroupBox);
            this.utilitiesGroupBox.Controls.Add(this.levelGroupBox);
            this.utilitiesGroupBox.Controls.Add(this.spritesGroupBox);
            this.utilitiesGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.utilitiesGroupBox.Location = new System.Drawing.Point(3, 3);
            this.utilitiesGroupBox.Name = "utilitiesGroupBox";
            this.utilitiesGroupBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.utilitiesGroupBox.Size = new System.Drawing.Size(379, 413);
            this.utilitiesGroupBox.TabIndex = 1;
            this.utilitiesGroupBox.TabStop = false;
            this.utilitiesGroupBox.Text = "Utilities";
            // 
            // playerGroupBox
            // 
            this.playerGroupBox.AutoSize = true;
            this.playerGroupBox.Controls.Add(this.playerTongueHitboxCheckBox);
            this.playerGroupBox.Controls.Add(this.playerEggInventoryCheckBox);
            this.playerGroupBox.Controls.Add(this.playerThrowInfoCheckBox);
            this.playerGroupBox.Controls.Add(this.playerBlockedStatusCheckBox);
            this.playerGroupBox.Controls.Add(this.playerSolidInteractionCheckBox);
            this.playerGroupBox.Controls.Add(this.playerHitboxCheckBox);
            this.playerGroupBox.Controls.Add(this.playerInfoCheckBox);
            this.playerGroupBox.Location = new System.Drawing.Point(6, 19);
            this.playerGroupBox.Name = "playerGroupBox";
            this.playerGroupBox.Size = new System.Drawing.Size(122, 193);
            this.playerGroupBox.TabIndex = 0;
            this.playerGroupBox.TabStop = false;
            this.playerGroupBox.Text = "Player";
            // 
            // playerTongueHitboxCheckBox
            // 
            this.playerTongueHitboxCheckBox.AutoSize = true;
            this.playerTongueHitboxCheckBox.Location = new System.Drawing.Point(6, 157);
            this.playerTongueHitboxCheckBox.Name = "playerTongueHitboxCheckBox";
            this.playerTongueHitboxCheckBox.Size = new System.Drawing.Size(94, 17);
            this.playerTongueHitboxCheckBox.TabIndex = 1;
            this.playerTongueHitboxCheckBox.Text = "Tongue hitbox";
            this.playerTongueHitboxCheckBox.UseVisualStyleBackColor = true;
            // 
            // playerEggInventoryCheckBox
            // 
            this.playerEggInventoryCheckBox.AutoSize = true;
            this.playerEggInventoryCheckBox.Location = new System.Drawing.Point(6, 134);
            this.playerEggInventoryCheckBox.Name = "playerEggInventoryCheckBox";
            this.playerEggInventoryCheckBox.Size = new System.Drawing.Size(91, 17);
            this.playerEggInventoryCheckBox.TabIndex = 1;
            this.playerEggInventoryCheckBox.Text = "Egg inventory";
            this.playerEggInventoryCheckBox.UseVisualStyleBackColor = true;
            // 
            // playerThrowInfoCheckBox
            // 
            this.playerThrowInfoCheckBox.AutoSize = true;
            this.playerThrowInfoCheckBox.Location = new System.Drawing.Point(6, 111);
            this.playerThrowInfoCheckBox.Name = "playerThrowInfoCheckBox";
            this.playerThrowInfoCheckBox.Size = new System.Drawing.Size(76, 17);
            this.playerThrowInfoCheckBox.TabIndex = 1;
            this.playerThrowInfoCheckBox.Text = "Throw info";
            this.playerThrowInfoCheckBox.UseVisualStyleBackColor = true;
            // 
            // playerBlockedStatusCheckBox
            // 
            this.playerBlockedStatusCheckBox.AutoSize = true;
            this.playerBlockedStatusCheckBox.Location = new System.Drawing.Point(6, 88);
            this.playerBlockedStatusCheckBox.Name = "playerBlockedStatusCheckBox";
            this.playerBlockedStatusCheckBox.Size = new System.Drawing.Size(96, 17);
            this.playerBlockedStatusCheckBox.TabIndex = 1;
            this.playerBlockedStatusCheckBox.Text = "Blocked status";
            this.playerBlockedStatusCheckBox.UseVisualStyleBackColor = true;
            // 
            // playerSolidInteractionCheckBox
            // 
            this.playerSolidInteractionCheckBox.AutoSize = true;
            this.playerSolidInteractionCheckBox.Location = new System.Drawing.Point(6, 65);
            this.playerSolidInteractionCheckBox.Name = "playerSolidInteractionCheckBox";
            this.playerSolidInteractionCheckBox.Size = new System.Drawing.Size(101, 17);
            this.playerSolidInteractionCheckBox.TabIndex = 1;
            this.playerSolidInteractionCheckBox.Text = "Solid interaction";
            this.playerSolidInteractionCheckBox.UseVisualStyleBackColor = true;
            // 
            // playerHitboxCheckBox
            // 
            this.playerHitboxCheckBox.AutoSize = true;
            this.playerHitboxCheckBox.Location = new System.Drawing.Point(6, 42);
            this.playerHitboxCheckBox.Name = "playerHitboxCheckBox";
            this.playerHitboxCheckBox.Size = new System.Drawing.Size(56, 17);
            this.playerHitboxCheckBox.TabIndex = 1;
            this.playerHitboxCheckBox.Text = "Hitbox";
            this.playerHitboxCheckBox.UseVisualStyleBackColor = true;
            // 
            // playerInfoCheckBox
            // 
            this.playerInfoCheckBox.AutoSize = true;
            this.playerInfoCheckBox.Location = new System.Drawing.Point(6, 19);
            this.playerInfoCheckBox.Name = "playerInfoCheckBox";
            this.playerInfoCheckBox.Size = new System.Drawing.Size(44, 17);
            this.playerInfoCheckBox.TabIndex = 1;
            this.playerInfoCheckBox.Text = "Info";
            this.playerInfoCheckBox.UseVisualStyleBackColor = true;
            // 
            // levelGroupBox
            // 
            this.levelGroupBox.AutoSize = true;
            this.levelGroupBox.Controls.Add(this.levelLayoutCheckBox);
            this.levelGroupBox.Controls.Add(this.levelScreensCheckBox);
            this.levelGroupBox.Controls.Add(this.levelMapButton);
            this.levelGroupBox.Controls.Add(this.levelTileTypesCheckBox);
            this.levelGroupBox.Controls.Add(this.levelTileGridCheckBox);
            this.levelGroupBox.Controls.Add(this.levelSpriteDataCheckBox);
            this.levelGroupBox.Controls.Add(this.levelExtraCheckBox);
            this.levelGroupBox.Controls.Add(this.levelInfoCheckBox);
            this.levelGroupBox.Location = new System.Drawing.Point(254, 19);
            this.levelGroupBox.Name = "levelGroupBox";
            this.levelGroupBox.Size = new System.Drawing.Size(119, 222);
            this.levelGroupBox.TabIndex = 0;
            this.levelGroupBox.TabStop = false;
            this.levelGroupBox.Text = "Level";
            // 
            // levelLayoutCheckBox
            // 
            this.levelLayoutCheckBox.AutoSize = true;
            this.levelLayoutCheckBox.Location = new System.Drawing.Point(6, 157);
            this.levelLayoutCheckBox.Name = "levelLayoutCheckBox";
            this.levelLayoutCheckBox.Size = new System.Drawing.Size(58, 17);
            this.levelLayoutCheckBox.TabIndex = 4;
            this.levelLayoutCheckBox.Text = "Layout";
            this.levelLayoutCheckBox.UseVisualStyleBackColor = true;
            // 
            // levelScreensCheckBox
            // 
            this.levelScreensCheckBox.AutoSize = true;
            this.levelScreensCheckBox.Location = new System.Drawing.Point(6, 134);
            this.levelScreensCheckBox.Name = "levelScreensCheckBox";
            this.levelScreensCheckBox.Size = new System.Drawing.Size(65, 17);
            this.levelScreensCheckBox.TabIndex = 3;
            this.levelScreensCheckBox.Text = "Screens";
            this.levelScreensCheckBox.UseVisualStyleBackColor = true;
            // 
            // levelMapButton
            // 
            this.levelMapButton.Location = new System.Drawing.Point(6, 180);
            this.levelMapButton.Name = "levelMapButton";
            this.levelMapButton.Size = new System.Drawing.Size(75, 23);
            this.levelMapButton.TabIndex = 2;
            this.levelMapButton.Text = "Level map";
            this.levelMapButton.UseVisualStyleBackColor = true;
            // 
            // levelTileTypesCheckBox
            // 
            this.levelTileTypesCheckBox.AutoSize = true;
            this.levelTileTypesCheckBox.Location = new System.Drawing.Point(6, 111);
            this.levelTileTypesCheckBox.Name = "levelTileTypesCheckBox";
            this.levelTileTypesCheckBox.Size = new System.Drawing.Size(71, 17);
            this.levelTileTypesCheckBox.TabIndex = 1;
            this.levelTileTypesCheckBox.Text = "Tile types";
            this.levelTileTypesCheckBox.UseVisualStyleBackColor = true;
            // 
            // levelTileGridCheckBox
            // 
            this.levelTileGridCheckBox.AutoSize = true;
            this.levelTileGridCheckBox.Location = new System.Drawing.Point(6, 88);
            this.levelTileGridCheckBox.Name = "levelTileGridCheckBox";
            this.levelTileGridCheckBox.Size = new System.Drawing.Size(63, 17);
            this.levelTileGridCheckBox.TabIndex = 1;
            this.levelTileGridCheckBox.Text = "Tile grid";
            this.levelTileGridCheckBox.UseVisualStyleBackColor = true;
            // 
            // levelSpriteDataCheckBox
            // 
            this.levelSpriteDataCheckBox.AutoSize = true;
            this.levelSpriteDataCheckBox.Location = new System.Drawing.Point(6, 42);
            this.levelSpriteDataCheckBox.Name = "levelSpriteDataCheckBox";
            this.levelSpriteDataCheckBox.Size = new System.Drawing.Size(77, 17);
            this.levelSpriteDataCheckBox.TabIndex = 1;
            this.levelSpriteDataCheckBox.Text = "Sprite data";
            this.levelSpriteDataCheckBox.UseVisualStyleBackColor = true;
            // 
            // levelExtraCheckBox
            // 
            this.levelExtraCheckBox.AutoSize = true;
            this.levelExtraCheckBox.Location = new System.Drawing.Point(6, 65);
            this.levelExtraCheckBox.Name = "levelExtraCheckBox";
            this.levelExtraCheckBox.Size = new System.Drawing.Size(50, 17);
            this.levelExtraCheckBox.TabIndex = 1;
            this.levelExtraCheckBox.Text = "Extra";
            this.levelExtraCheckBox.UseVisualStyleBackColor = true;
            // 
            // levelInfoCheckBox
            // 
            this.levelInfoCheckBox.AutoSize = true;
            this.levelInfoCheckBox.Location = new System.Drawing.Point(6, 19);
            this.levelInfoCheckBox.Name = "levelInfoCheckBox";
            this.levelInfoCheckBox.Size = new System.Drawing.Size(44, 17);
            this.levelInfoCheckBox.TabIndex = 1;
            this.levelInfoCheckBox.Text = "Info";
            this.levelInfoCheckBox.UseVisualStyleBackColor = true;
            // 
            // spritesGroupBox
            // 
            this.spritesGroupBox.AutoSize = true;
            this.spritesGroupBox.Controls.Add(this.spriteTablesButton);
            this.spritesGroupBox.Controls.Add(this.spritesSpawningAreasCheckBox);
            this.spritesGroupBox.Controls.Add(this.spritesSpecialInfoCheckBox);
            this.spritesGroupBox.Controls.Add(this.spritesTableCheckBox);
            this.spritesGroupBox.Controls.Add(this.spritesHitboxCheckBox);
            this.spritesGroupBox.Controls.Add(this.spritesInfoCheckBox);
            this.spritesGroupBox.Location = new System.Drawing.Point(134, 19);
            this.spritesGroupBox.Name = "spritesGroupBox";
            this.spritesGroupBox.Size = new System.Drawing.Size(114, 176);
            this.spritesGroupBox.TabIndex = 0;
            this.spritesGroupBox.TabStop = false;
            this.spritesGroupBox.Text = "Sprites";
            // 
            // spriteTablesButton
            // 
            this.spriteTablesButton.Location = new System.Drawing.Point(6, 134);
            this.spriteTablesButton.Name = "spriteTablesButton";
            this.spriteTablesButton.Size = new System.Drawing.Size(75, 23);
            this.spriteTablesButton.TabIndex = 2;
            this.spriteTablesButton.Text = "Sprite tables";
            this.spriteTablesButton.UseVisualStyleBackColor = true;
            // 
            // spritesSpawningAreasCheckBox
            // 
            this.spritesSpawningAreasCheckBox.AutoSize = true;
            this.spritesSpawningAreasCheckBox.Location = new System.Drawing.Point(6, 111);
            this.spritesSpawningAreasCheckBox.Name = "spritesSpawningAreasCheckBox";
            this.spritesSpawningAreasCheckBox.Size = new System.Drawing.Size(102, 17);
            this.spritesSpawningAreasCheckBox.TabIndex = 1;
            this.spritesSpawningAreasCheckBox.Text = "Spawning areas";
            this.spritesSpawningAreasCheckBox.UseVisualStyleBackColor = true;
            // 
            // spritesSpecialInfoCheckBox
            // 
            this.spritesSpecialInfoCheckBox.AutoSize = true;
            this.spritesSpecialInfoCheckBox.Location = new System.Drawing.Point(6, 88);
            this.spritesSpecialInfoCheckBox.Name = "spritesSpecialInfoCheckBox";
            this.spritesSpecialInfoCheckBox.Size = new System.Drawing.Size(81, 17);
            this.spritesSpecialInfoCheckBox.TabIndex = 1;
            this.spritesSpecialInfoCheckBox.Text = "Special info";
            this.spritesSpecialInfoCheckBox.UseVisualStyleBackColor = true;
            // 
            // spritesTableCheckBox
            // 
            this.spritesTableCheckBox.AutoSize = true;
            this.spritesTableCheckBox.Location = new System.Drawing.Point(6, 42);
            this.spritesTableCheckBox.Name = "spritesTableCheckBox";
            this.spritesTableCheckBox.Size = new System.Drawing.Size(53, 17);
            this.spritesTableCheckBox.TabIndex = 1;
            this.spritesTableCheckBox.Text = "Table";
            this.spritesTableCheckBox.UseVisualStyleBackColor = true;
            // 
            // spritesHitboxCheckBox
            // 
            this.spritesHitboxCheckBox.AutoSize = true;
            this.spritesHitboxCheckBox.Location = new System.Drawing.Point(6, 65);
            this.spritesHitboxCheckBox.Name = "spritesHitboxCheckBox";
            this.spritesHitboxCheckBox.Size = new System.Drawing.Size(56, 17);
            this.spritesHitboxCheckBox.TabIndex = 1;
            this.spritesHitboxCheckBox.Text = "Hitbox";
            this.spritesHitboxCheckBox.UseVisualStyleBackColor = true;
            // 
            // spritesInfoCheckBox
            // 
            this.spritesInfoCheckBox.AutoSize = true;
            this.spritesInfoCheckBox.Location = new System.Drawing.Point(6, 19);
            this.spritesInfoCheckBox.Name = "spritesInfoCheckBox";
            this.spritesInfoCheckBox.Size = new System.Drawing.Size(44, 17);
            this.spritesInfoCheckBox.TabIndex = 1;
            this.spritesInfoCheckBox.Text = "Info";
            this.spritesInfoCheckBox.UseVisualStyleBackColor = true;
            // 
            // settingsGroupBox
            // 
            this.settingsGroupBox.Controls.Add(this.gapsLabel);
            this.settingsGroupBox.Controls.Add(this.bottomGapNumericUpDown);
            this.settingsGroupBox.Controls.Add(this.rightGapNumericUpDown);
            this.settingsGroupBox.Controls.Add(this.topGapNumericUpDown);
            this.settingsGroupBox.Controls.Add(this.leftGapNumericUpDown);
            this.settingsGroupBox.Controls.Add(this.filterNumericUpDown);
            this.settingsGroupBox.Controls.Add(this.darkFilterCheckBox);
            this.settingsGroupBox.Controls.Add(this.eraseTilesButton);
            this.settingsGroupBox.Controls.Add(this.mouseInfoCheckBox);
            this.settingsGroupBox.Controls.Add(this.drawTilesCheckBox);
            this.settingsGroupBox.Location = new System.Drawing.Point(3, 422);
            this.settingsGroupBox.Name = "settingsGroupBox";
            this.settingsGroupBox.Size = new System.Drawing.Size(379, 103);
            this.settingsGroupBox.TabIndex = 2;
            this.settingsGroupBox.TabStop = false;
            this.settingsGroupBox.Text = "Settings";
            // 
            // gapsLabel
            // 
            this.gapsLabel.AutoSize = true;
            this.gapsLabel.Location = new System.Drawing.Point(230, 44);
            this.gapsLabel.Name = "gapsLabel";
            this.gapsLabel.Size = new System.Drawing.Size(32, 13);
            this.gapsLabel.TabIndex = 6;
            this.gapsLabel.Text = "Gaps";
            // 
            // bottomGapNumericUpDown
            // 
            this.bottomGapNumericUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.bottomGapNumericUpDown.Location = new System.Drawing.Point(222, 68);
            this.bottomGapNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.bottomGapNumericUpDown.Name = "bottomGapNumericUpDown";
            this.bottomGapNumericUpDown.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bottomGapNumericUpDown.Size = new System.Drawing.Size(48, 20);
            this.bottomGapNumericUpDown.TabIndex = 5;
            this.bottomGapNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.bottomGapNumericUpDown.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // rightGapNumericUpDown
            // 
            this.rightGapNumericUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.rightGapNumericUpDown.Location = new System.Drawing.Point(270, 41);
            this.rightGapNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.rightGapNumericUpDown.Name = "rightGapNumericUpDown";
            this.rightGapNumericUpDown.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rightGapNumericUpDown.Size = new System.Drawing.Size(48, 20);
            this.rightGapNumericUpDown.TabIndex = 5;
            this.rightGapNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.rightGapNumericUpDown.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // topGapNumericUpDown
            // 
            this.topGapNumericUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.topGapNumericUpDown.Location = new System.Drawing.Point(222, 15);
            this.topGapNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.topGapNumericUpDown.Name = "topGapNumericUpDown";
            this.topGapNumericUpDown.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.topGapNumericUpDown.Size = new System.Drawing.Size(48, 20);
            this.topGapNumericUpDown.TabIndex = 5;
            this.topGapNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.topGapNumericUpDown.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // leftGapNumericUpDown
            // 
            this.leftGapNumericUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.leftGapNumericUpDown.Location = new System.Drawing.Point(173, 41);
            this.leftGapNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.leftGapNumericUpDown.Name = "leftGapNumericUpDown";
            this.leftGapNumericUpDown.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.leftGapNumericUpDown.Size = new System.Drawing.Size(48, 20);
            this.leftGapNumericUpDown.TabIndex = 5;
            this.leftGapNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.leftGapNumericUpDown.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // filterNumericUpDown
            // 
            this.filterNumericUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.filterNumericUpDown.Location = new System.Drawing.Point(85, 68);
            this.filterNumericUpDown.Name = "filterNumericUpDown";
            this.filterNumericUpDown.Size = new System.Drawing.Size(41, 20);
            this.filterNumericUpDown.TabIndex = 4;
            this.filterNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.filterNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // darkFilterCheckBox
            // 
            this.darkFilterCheckBox.AutoSize = true;
            this.darkFilterCheckBox.Location = new System.Drawing.Point(7, 68);
            this.darkFilterCheckBox.Name = "darkFilterCheckBox";
            this.darkFilterCheckBox.Size = new System.Drawing.Size(71, 17);
            this.darkFilterCheckBox.TabIndex = 3;
            this.darkFilterCheckBox.Text = "Dark filter";
            this.darkFilterCheckBox.UseVisualStyleBackColor = true;
            // 
            // eraseTilesButton
            // 
            this.eraseTilesButton.Location = new System.Drawing.Point(85, 16);
            this.eraseTilesButton.Name = "eraseTilesButton";
            this.eraseTilesButton.Size = new System.Drawing.Size(46, 23);
            this.eraseTilesButton.TabIndex = 2;
            this.eraseTilesButton.Text = "Erase";
            this.eraseTilesButton.UseVisualStyleBackColor = true;
            // 
            // mouseInfoCheckBox
            // 
            this.mouseInfoCheckBox.AutoSize = true;
            this.mouseInfoCheckBox.Location = new System.Drawing.Point(7, 44);
            this.mouseInfoCheckBox.Name = "mouseInfoCheckBox";
            this.mouseInfoCheckBox.Size = new System.Drawing.Size(78, 17);
            this.mouseInfoCheckBox.TabIndex = 1;
            this.mouseInfoCheckBox.Text = "Mouse info";
            this.mouseInfoCheckBox.UseVisualStyleBackColor = true;
            // 
            // drawTilesCheckBox
            // 
            this.drawTilesCheckBox.AutoSize = true;
            this.drawTilesCheckBox.Location = new System.Drawing.Point(7, 20);
            this.drawTilesCheckBox.Name = "drawTilesCheckBox";
            this.drawTilesCheckBox.Size = new System.Drawing.Size(72, 17);
            this.drawTilesCheckBox.TabIndex = 0;
            this.drawTilesCheckBox.Text = "Draw tiles";
            this.drawTilesCheckBox.UseVisualStyleBackColor = true;
            // 
            // generalGroupBox
            // 
            this.generalGroupBox.AutoSize = true;
            this.generalGroupBox.Controls.Add(this.creditsWarpHelperCheckBox);
            this.generalGroupBox.Controls.Add(this.movieInfoCheckBox);
            this.generalGroupBox.Controls.Add(this.countersCheckBox);
            this.generalGroupBox.Controls.Add(this.miscInfoCheckBox);
            this.generalGroupBox.Controls.Add(this.overworldInfoCheckBox);
            this.generalGroupBox.Location = new System.Drawing.Point(6, 218);
            this.generalGroupBox.Name = "generalGroupBox";
            this.generalGroupBox.Size = new System.Drawing.Size(122, 147);
            this.generalGroupBox.TabIndex = 2;
            this.generalGroupBox.TabStop = false;
            this.generalGroupBox.Text = "General";
            // 
            // creditsWarpHelperCheckBox
            // 
            this.creditsWarpHelperCheckBox.AutoSize = true;
            this.creditsWarpHelperCheckBox.Location = new System.Drawing.Point(6, 111);
            this.creditsWarpHelperCheckBox.Name = "creditsWarpHelperCheckBox";
            this.creditsWarpHelperCheckBox.Size = new System.Drawing.Size(110, 17);
            this.creditsWarpHelperCheckBox.TabIndex = 1;
            this.creditsWarpHelperCheckBox.Text = "Credits Warp help";
            this.creditsWarpHelperCheckBox.UseVisualStyleBackColor = true;
            // 
            // movieInfoCheckBox
            // 
            this.movieInfoCheckBox.AutoSize = true;
            this.movieInfoCheckBox.Location = new System.Drawing.Point(6, 88);
            this.movieInfoCheckBox.Name = "movieInfoCheckBox";
            this.movieInfoCheckBox.Size = new System.Drawing.Size(75, 17);
            this.movieInfoCheckBox.TabIndex = 1;
            this.movieInfoCheckBox.Text = "Movie info";
            this.movieInfoCheckBox.UseVisualStyleBackColor = true;
            // 
            // countersCheckBox
            // 
            this.countersCheckBox.AutoSize = true;
            this.countersCheckBox.Location = new System.Drawing.Point(6, 65);
            this.countersCheckBox.Name = "countersCheckBox";
            this.countersCheckBox.Size = new System.Drawing.Size(88, 17);
            this.countersCheckBox.TabIndex = 1;
            this.countersCheckBox.Text = "Counters info";
            this.countersCheckBox.UseVisualStyleBackColor = true;
            // 
            // miscInfoCheckBox
            // 
            this.miscInfoCheckBox.AutoSize = true;
            this.miscInfoCheckBox.Location = new System.Drawing.Point(6, 42);
            this.miscInfoCheckBox.Name = "miscInfoCheckBox";
            this.miscInfoCheckBox.Size = new System.Drawing.Size(93, 17);
            this.miscInfoCheckBox.TabIndex = 1;
            this.miscInfoCheckBox.Text = "Miscellaneous";
            this.miscInfoCheckBox.UseVisualStyleBackColor = true;
            // 
            // overworldInfoCheckBox
            // 
            this.overworldInfoCheckBox.AutoSize = true;
            this.overworldInfoCheckBox.Location = new System.Drawing.Point(6, 19);
            this.overworldInfoCheckBox.Name = "overworldInfoCheckBox";
            this.overworldInfoCheckBox.Size = new System.Drawing.Size(94, 17);
            this.overworldInfoCheckBox.TabIndex = 1;
            this.overworldInfoCheckBox.Text = "Overworld info";
            this.overworldInfoCheckBox.UseVisualStyleBackColor = true;
            // 
            // ambientSpritesGroupBox
            // 
            this.ambientSpritesGroupBox.AutoSize = true;
            this.ambientSpritesGroupBox.Controls.Add(this.ambientSpritesSlotInScreenCheckBox);
            this.ambientSpritesGroupBox.Controls.Add(this.ambientSpritesTableCheckBox);
            this.ambientSpritesGroupBox.Controls.Add(this.ambientSpritesInfoCheckBox);
            this.ambientSpritesGroupBox.Location = new System.Drawing.Point(134, 201);
            this.ambientSpritesGroupBox.Name = "ambientSpritesGroupBox";
            this.ambientSpritesGroupBox.Size = new System.Drawing.Size(114, 101);
            this.ambientSpritesGroupBox.TabIndex = 3;
            this.ambientSpritesGroupBox.TabStop = false;
            this.ambientSpritesGroupBox.Text = "Ambient sprites";
            // 
            // ambientSpritesSlotInScreenCheckBox
            // 
            this.ambientSpritesSlotInScreenCheckBox.AutoSize = true;
            this.ambientSpritesSlotInScreenCheckBox.Location = new System.Drawing.Point(6, 65);
            this.ambientSpritesSlotInScreenCheckBox.Name = "ambientSpritesSlotInScreenCheckBox";
            this.ambientSpritesSlotInScreenCheckBox.Size = new System.Drawing.Size(99, 17);
            this.ambientSpritesSlotInScreenCheckBox.TabIndex = 1;
            this.ambientSpritesSlotInScreenCheckBox.Text = "Slots on screen";
            this.ambientSpritesSlotInScreenCheckBox.UseVisualStyleBackColor = true;
            // 
            // ambientSpritesTableCheckBox
            // 
            this.ambientSpritesTableCheckBox.AutoSize = true;
            this.ambientSpritesTableCheckBox.Location = new System.Drawing.Point(6, 42);
            this.ambientSpritesTableCheckBox.Name = "ambientSpritesTableCheckBox";
            this.ambientSpritesTableCheckBox.Size = new System.Drawing.Size(53, 17);
            this.ambientSpritesTableCheckBox.TabIndex = 1;
            this.ambientSpritesTableCheckBox.Text = "Table";
            this.ambientSpritesTableCheckBox.UseVisualStyleBackColor = true;
            // 
            // ambientSpritesInfoCheckBox
            // 
            this.ambientSpritesInfoCheckBox.AutoSize = true;
            this.ambientSpritesInfoCheckBox.Location = new System.Drawing.Point(6, 19);
            this.ambientSpritesInfoCheckBox.Name = "ambientSpritesInfoCheckBox";
            this.ambientSpritesInfoCheckBox.Size = new System.Drawing.Size(44, 17);
            this.ambientSpritesInfoCheckBox.TabIndex = 1;
            this.ambientSpritesInfoCheckBox.Text = "Info";
            this.ambientSpritesInfoCheckBox.UseVisualStyleBackColor = true;
            // 
            // debugGroupBox
            // 
            this.debugGroupBox.AutoSize = true;
            this.debugGroupBox.Controls.Add(this.lagmeterCheckBox);
            this.debugGroupBox.Controls.Add(this.debugControllerDataCheckBox);
            this.debugGroupBox.Controls.Add(this.spriteLoadStatusCheckBox);
            this.debugGroupBox.Controls.Add(this.debugSpriteExtraCheckBox);
            this.debugGroupBox.Controls.Add(this.debugPlayerExtraCheckBox);
            this.debugGroupBox.Location = new System.Drawing.Point(254, 247);
            this.debugGroupBox.Name = "debugGroupBox";
            this.debugGroupBox.Size = new System.Drawing.Size(119, 147);
            this.debugGroupBox.TabIndex = 3;
            this.debugGroupBox.TabStop = false;
            this.debugGroupBox.Text = "Debug";
            // 
            // lagmeterCheckBox
            // 
            this.lagmeterCheckBox.AutoSize = true;
            this.lagmeterCheckBox.Location = new System.Drawing.Point(6, 111);
            this.lagmeterCheckBox.Name = "lagmeterCheckBox";
            this.lagmeterCheckBox.Size = new System.Drawing.Size(70, 17);
            this.lagmeterCheckBox.TabIndex = 1;
            this.lagmeterCheckBox.Text = "Lagmeter";
            this.lagmeterCheckBox.UseVisualStyleBackColor = true;
            // 
            // debugControllerDataCheckBox
            // 
            this.debugControllerDataCheckBox.AutoSize = true;
            this.debugControllerDataCheckBox.Location = new System.Drawing.Point(6, 88);
            this.debugControllerDataCheckBox.Name = "debugControllerDataCheckBox";
            this.debugControllerDataCheckBox.Size = new System.Drawing.Size(94, 17);
            this.debugControllerDataCheckBox.TabIndex = 1;
            this.debugControllerDataCheckBox.Text = "Controller data";
            this.debugControllerDataCheckBox.UseVisualStyleBackColor = true;
            // 
            // spriteLoadStatusCheckBox
            // 
            this.spriteLoadStatusCheckBox.AutoSize = true;
            this.spriteLoadStatusCheckBox.Location = new System.Drawing.Point(6, 65);
            this.spriteLoadStatusCheckBox.Name = "spriteLoadStatusCheckBox";
            this.spriteLoadStatusCheckBox.Size = new System.Drawing.Size(107, 17);
            this.spriteLoadStatusCheckBox.TabIndex = 1;
            this.spriteLoadStatusCheckBox.Text = "Sprite load status";
            this.spriteLoadStatusCheckBox.UseVisualStyleBackColor = true;
            // 
            // debugSpriteExtraCheckBox
            // 
            this.debugSpriteExtraCheckBox.AutoSize = true;
            this.debugSpriteExtraCheckBox.Location = new System.Drawing.Point(6, 42);
            this.debugSpriteExtraCheckBox.Name = "debugSpriteExtraCheckBox";
            this.debugSpriteExtraCheckBox.Size = new System.Drawing.Size(79, 17);
            this.debugSpriteExtraCheckBox.TabIndex = 1;
            this.debugSpriteExtraCheckBox.Text = "Sprite extra";
            this.debugSpriteExtraCheckBox.UseVisualStyleBackColor = true;
            // 
            // debugPlayerExtraCheckBox
            // 
            this.debugPlayerExtraCheckBox.AutoSize = true;
            this.debugPlayerExtraCheckBox.Location = new System.Drawing.Point(6, 19);
            this.debugPlayerExtraCheckBox.Name = "debugPlayerExtraCheckBox";
            this.debugPlayerExtraCheckBox.Size = new System.Drawing.Size(81, 17);
            this.debugPlayerExtraCheckBox.TabIndex = 1;
            this.debugPlayerExtraCheckBox.Text = "Player extra";
            this.debugPlayerExtraCheckBox.UseVisualStyleBackColor = true;
            // 
            // MainToolForm
            // 
            this.ClientSize = new System.Drawing.Size(410, 553);
            this.Controls.Add(this.utilitiesFlowLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainToolForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.utilitiesFlowLayoutPanel.ResumeLayout(false);
            this.utilitiesFlowLayoutPanel.PerformLayout();
            this.utilitiesGroupBox.ResumeLayout(false);
            this.utilitiesGroupBox.PerformLayout();
            this.playerGroupBox.ResumeLayout(false);
            this.playerGroupBox.PerformLayout();
            this.levelGroupBox.ResumeLayout(false);
            this.levelGroupBox.PerformLayout();
            this.spritesGroupBox.ResumeLayout(false);
            this.spritesGroupBox.PerformLayout();
            this.settingsGroupBox.ResumeLayout(false);
            this.settingsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bottomGapNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightGapNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.topGapNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftGapNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterNumericUpDown)).EndInit();
            this.generalGroupBox.ResumeLayout(false);
            this.generalGroupBox.PerformLayout();
            this.ambientSpritesGroupBox.ResumeLayout(false);
            this.ambientSpritesGroupBox.PerformLayout();
            this.debugGroupBox.ResumeLayout(false);
            this.debugGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        private void SetBindings()
        {
            Dictionary<Control, string> controlBindingMap = new Dictionary<Control, string>()
            {
                // Player
                { playerInfoCheckBox, nameof(_toolOptions.DisplayPlayerInfo) },
                { playerHitboxCheckBox, nameof(_toolOptions.DisplayPlayerHitbox) },
                { playerSolidInteractionCheckBox, nameof(_toolOptions.DisplayInteractionPoints) },
                { playerBlockedStatusCheckBox, nameof(_toolOptions.DisplayBlockedStatus) },
                { playerThrowInfoCheckBox, nameof(_toolOptions.DisplayThrowInfo) },
                { playerEggInventoryCheckBox, nameof(_toolOptions.DisplayEggInfo) },
                { playerTongueHitboxCheckBox, nameof(_toolOptions.DisplayTongueHitbox) },
                // Sprites
                { spritesInfoCheckBox, nameof(_toolOptions.DisplaySpriteInfo) },
                { spritesTableCheckBox, nameof(_toolOptions.DisplaySpriteTable) },
                { spritesHitboxCheckBox, nameof(_toolOptions.DisplaySpriteHitbox) },
                //{ spritesSlotInScreenCheckBox, nameof(_toolOptions.DisplaySpriteSlotInScreen) }, // TODO: Decide if will be really used
                { spritesSpecialInfoCheckBox, nameof(_toolOptions.DisplaySpriteSpecialInfo) },
                { spritesSpawningAreasCheckBox, nameof(_toolOptions.DisplaySpriteSpawningAreas) },
                // Level
                { levelInfoCheckBox, nameof(_toolOptions.DisplayLevelInfo) },
                { levelSpriteDataCheckBox, nameof(_toolOptions.DisplaySpriteData) },
                { levelExtraCheckBox, nameof(_toolOptions.DisplayLevelExtra) },
                { levelTileGridCheckBox, nameof(_toolOptions.DrawTileMapGrid) },
                { levelTileTypesCheckBox, nameof(_toolOptions.DrawTileMapType) },
                { levelScreensCheckBox, nameof(_toolOptions.DrawTileMapScreen) },
                { levelLayoutCheckBox, nameof(_toolOptions.DisplayLevelLayout) },
                // General
                { overworldInfoCheckBox, nameof(_toolOptions.DisplayOverworldInfo) },
                { miscInfoCheckBox, nameof(_toolOptions.DisplayMiscInfo) },
                { countersCheckBox, nameof(_toolOptions.DisplayCounters) },
                { movieInfoCheckBox, nameof(_toolOptions.DisplayMovieInfo) },
                { creditsWarpHelperCheckBox, nameof(_toolOptions.DisplayCreditsWarpHelper) },
                // Ambient sprites
                { ambientSpritesInfoCheckBox, nameof(_toolOptions.DisplayAmbientSpriteInfo) },
                { ambientSpritesTableCheckBox, nameof(_toolOptions.DisplayAmbientSpriteTable) },
                { ambientSpritesSlotInScreenCheckBox, nameof(_toolOptions.DisplayAmbientSpriteSlotInScreen) },
                // Debug
                { debugPlayerExtraCheckBox, nameof(_toolOptions.DisplayDebugPlayerExtra) },
                { debugSpriteExtraCheckBox, nameof(_toolOptions.DisplayDebugSpriteExtra) },
                //{ debugAmbientSpriteCheckBox, nameof(_toolOptions.DisplayDebugAmbientSprite) }, // TODO: Decide if will be really used
                { spriteLoadStatusCheckBox, nameof(_toolOptions.DisplaySpriteLoadStatus) },
                { debugControllerDataCheckBox, nameof(_toolOptions.DisplayDebugControllerData) },
                { lagmeterCheckBox, nameof(_toolOptions.DisplayLagmeter) },
                //{ debugInfoCheckBox, nameof(_toolOptions.DisplayDebugInfo) }, // TODO: Decide if will be really used
                // Settings
                //{ windowsDisplayScaleComboBox, nameof(_toolOptions.WindowsDisplayScale) },
                { drawTilesCheckBox, nameof(_toolOptions.DrawTilesWithClick) },
                //{ maxTilesDrawnTextBox, nameof(_toolOptions.MaxTilesDrawn) },
                { mouseInfoCheckBox, nameof(_toolOptions.DisplayMouseCoordinates) },
                { darkFilterCheckBox, nameof(_toolOptions.DrawDarkFilter) },
                { filterNumericUpDown, nameof(_toolOptions.DarkFilterOpacity) },
                { leftGapNumericUpDown, nameof(_toolOptions.LeftGap) },
                { rightGapNumericUpDown, nameof(_toolOptions.RightGap) },
                { topGapNumericUpDown, nameof(_toolOptions.TopGap) },
                { bottomGapNumericUpDown, nameof(_toolOptions.BottomGap) },

            };

            foreach (var binding in controlBindingMap)
            {
                Control control = binding.Key;
                string optionsBindingProperty = binding.Value;

                string controlBindingProperty = string.Empty;
                if (control is CheckBox checkBox)
                    controlBindingProperty = "Checked";
                else if (control is NumericUpDown numericUpDown)
                    controlBindingProperty = "Value";
                else if (control is TextBox textBox)
                    controlBindingProperty = "Text";

                control?.DataBindings.Add(controlBindingProperty, _toolOptions, optionsBindingProperty, true, DataSourceUpdateMode.OnPropertyChanged);
            }
        }
    }
}
