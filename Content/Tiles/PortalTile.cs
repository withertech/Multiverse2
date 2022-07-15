using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Multiverse2.Content.Configs;
using Multiverse2.Content.Configs.UI;
using Multiverse2.Content.Subworlds;
using SubworldLibrary;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.GameContent.ObjectInteractions;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.IO;
using Terraria.ObjectData;
using Terraria.UI;

namespace Multiverse2.Content.Tiles
{
    public class PortalTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileFrameImportant[Type] = true;
            TileID.Sets.HasOutlines[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;
            
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.HookPostPlaceMyPlayer =
                new PlacementHook(ModContent.GetInstance<PortalTileEntity>().Hook_AfterPlacement, -1, 0, false);
            TileObjectData.addTile(Type);
            AnimationFrameHeight = 56;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            if (++frameCounter >= 10)
            {
                frameCounter = 0;
                frame = ++frame % 5;
            }
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            var origin = TileUtils.GetTileOrigin(i, j);
            ModContent.GetInstance<PortalTileEntity>().Kill(origin.X, origin.Y);
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 48, 48,
                ModContent.ItemType<PortalTileItem>());
        }

        public override bool RightClick(int i, int j)
        {
            if (TileUtils.TryGetTileEntityAs(i, j, out PortalTileEntity entity))
            {
                string fullname = $"{entity.Subworld.Mod}/{entity.Subworld.Name}";
                if (fullname == "Terraria/None")
                {
                    SubworldSystem.Exit();
                }
                else
                {
                    SubworldSystem.Enter(fullname);
                }

                return true;
            }

            return false;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.0f;
            g = 1f;
            b = 0.1f;
        }

        public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
        {
            return true;
        }
    }

    public class PortalTileItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Multiverse Portal");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 14;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = 150;
            Item.createTile = ModContent.TileType<PortalTile>();
        }

        public override void AddRecipes()
        {
            if (ModContent.GetInstance<MultiverseConfig>().PortalRecipe)
                CreateRecipe()
                    .AddIngredient(ItemID.StoneBlock, 10)
                    .AddIngredient(ItemID.FallenStar, 5)
                    .AddTile(TileID.Hellforge)
                    .Register();
        }
    }

    public class PortalTileEntity : ModTileEntity
    {
        public SubworldDefinition Subworld { get; internal set; } = ModContent.GetContent<Subworld>().Any()
            ? new SubworldDefinition(-1)
            : new SubworldDefinition();

        public override bool IsTileValidForEntity(int x, int y)
        {
            var tile = Main.tile[x, y];
            return tile.HasTile && tile.TileType == ModContent.TileType<PortalTile>();
        }

        public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction, int alternate)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                const int width = 3;
                const int height = 3;
                NetMessage.SendTileSquare(Main.myPlayer, i, j, width, height);

                NetMessage.SendData(MessageID.TileEntityPlacement, -1, -1, null, i, j, Type);
            }

            var origin = TileUtils.GetTileOrigin(i, j);
            var placedEntity = Place(origin.X, origin.Y);
            var player = Main.LocalPlayer;

            if (SubworldSystem.Current == null)
            {
//Should your tile entity bring up a UI, this line is useful to prevent item slots from misbehaving
                Main.mouseRightRelease = false;

                //The following four (4) if-blocks are recommended to be used if your multitile opens a UI when right clicked:
                if (player.sign > -1)
                {
                    SoundEngine.PlaySound(SoundID.MenuClose);
                    player.sign = -1;
                    Main.editSign = false;
                    Main.npcChatText = string.Empty;
                }

                if (Main.editChest)
                {
                    SoundEngine.PlaySound(SoundID.MenuTick);
                    Main.editChest = false;
                    Main.npcChatText = string.Empty;
                }

                if (player.editedChestName)
                {
                    NetMessage.SendData(MessageID.SyncPlayerChest, -1, -1,
                        NetworkText.FromLiteral(Main.chest[player.chest].name), player.chest, 1f);
                    player.editedChestName = false;
                }

                if (player.talkNPC > -1)
                {
                    player.SetTalkNPC(-1);
                    Main.npcChatCornerItem = 0;
                    Main.npcChatText = string.Empty;
                }


                ModContent.GetInstance<MultiverseSystem>().UIState.Show(origin);
            }
            else
            {
                if (TileUtils.TryGetTileEntityAs(origin.X, origin.Y, out PortalTileEntity entity))
                {
                    entity.Subworld = new SubworldDefinition();
                }
            }

            return placedEntity;
        }

        public override void OnNetPlace()
        {
            if (Main.netMode == NetmodeID.Server)
                NetMessage.SendData(MessageID.TileEntitySharing, -1, -1, null, ID, Position.X, Position.Y);
        }

        public override void SaveData(TagCompound tag)
        {
            tag.Set("Subworld", Subworld.SerializeData());
        }

        public override void LoadData(TagCompound tag)
        {
            Subworld = SubworldDefinition.Load(tag.GetCompound("Subworld"));
        }
    }

    public class PortalUI : UIState
    {
        private SubworldDefinitionElement _picker;
        private Point16 _pos;

        public void Show(Point16 pos)
        {

            if (ModContent.GetContent<Subworld>().Any() &&
                TileUtils.TryGetTileEntityAs(pos.X, pos.Y, out PortalTileEntity entity))
            {
                _pos = pos;
                foreach (var fieldsAndProperty in ConfigManager.GetFieldsAndProperties(entity))
                    if (fieldsAndProperty.Type == typeof(SubworldDefinition))
                    {
                        _picker.Bind(fieldsAndProperty, entity, null, -1);
                        _picker.OnBind();
                    }
            }

            ModContent.GetInstance<MultiverseSystem>().UI.SetState(this);
        }

        public void Hide()
        {
            ModContent.GetInstance<MultiverseSystem>().UI.SetState(null);
        }

        public void Save()
        {
            if (TileUtils.TryGetTileEntityAs(_pos.X, _pos.Y, out PortalTileEntity entity) && entity.Subworld.Type == -1)
            {
                WorldGen.KillTile(_pos.X, _pos.Y);
            }
            Hide();
        }

        public override void OnInitialize()
        {
            var panel = new UIPanel();
            panel.Width.Set(600, 0);
            panel.Height.Set(300, 0);
            panel.HAlign = panel.VAlign = 0.5f;
            Append(panel);

            var list = new UIList
            {
                HAlign = 0.5f,
                VAlign = 0.5f,
                Width =
                {
                    Percent = 1
                },
                Height =
                {
                    Percent = 1
                }
            };
            panel.Append(list);

            var header = new UIText("Multiverse Portal")
            {
                HAlign = 0.5f
            };
            list.Add(header);

            if (ModContent.GetContent<Subworld>().Any())
            {
                _picker = new SubworldDefinitionElement(definition =>
                    ModContent.GetInstance<MultiverseConfig>() != null &&
                    ModContent.GetInstance<MultiverseConfig>().TpFilter.Exists(configuration =>
                        configuration.Subworld.Mod == definition.Mod &&
                        configuration.Subworld.Name == definition.Name && configuration.Portal) ||
                    !ModContent.GetInstance<MultiverseConfig>().TpFilter.Exists(configuration =>
                        configuration.Subworld.Mod == definition.Mod && configuration.Subworld.Name == definition.Name))
                {
                    HAlign = 0.5f
                };
                list.Add(_picker);
            }
            else
            {
                list.Add(new UITextPanel<string>(
                    "ERROR: No Subworlds Found. \nEither add one in the config, or install a mod that adds one")
                {
                    Width =
                    {
                        Precent = 1
                    },
                    Height =
                    {
                        Pixels = 60
                    },
                    HAlign = 0.5f
                });
            }

            var saveButton = new UIIconTextButton(Language.GetText("Mods.Multiverse2.Save"), Color.White,
                "Images/UI/CharCreation/Paste")
            {
                Width =
                {
                    Precent = 1
                },
                Height =
                {
                    Pixels = 30
                },
                HAlign = 0.5f
            };

            saveButton.OnClick += delegate { Save(); };
            list.Add(saveButton);
        }
    }
}