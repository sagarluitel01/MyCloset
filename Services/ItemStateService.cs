using MyCloset.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Diagnostics;
using Microsoft.Maui.Storage;

namespace MyCloset.Services
{

    public class ItemStateService
    {
        public List<Item> Items { get; } = new List<Item>();

        private readonly List<ItemStatus> _statuses = new List<ItemStatus>
        {
            new ItemStatus { Name = "New", Description = "New Item, recently Purchased." },
            new ItemStatus { Name = "Washed", Description = "Item has been washed and is ready to wear." },
            new ItemStatus { Name = "Worn", Description = "Item has been worn." },
            new ItemStatus { Name = "ReadyForWash", Description = "Item is ready to be washed." }
        };

        private readonly string _itemsFilePath;
        private readonly object _fileLock = new object();

        public ItemStateService()
        {
            _itemsFilePath = Path.Combine(FileSystem.AppDataDirectory, "items.json");
            LoadItemsFromFile();
        }

        private void LoadItemsFromFile()
        {
            lock (_fileLock)
            {
                try
                {
                    if (!File.Exists(_itemsFilePath))
                        return;

                    var json = File.ReadAllText(_itemsFilePath);
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var items = JsonSerializer.Deserialize<List<Item>>(json, options);
                    if (items != null)
                    {
                        Items.Clear();
                        Items.AddRange(items);

                        // ensure statuses list includes any statuses found in the saved items
                        foreach (var s in Items.Select(i => i.Status).Where(s => s != null))
                        {
                            AddStatus(s);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed to load items from {_itemsFilePath}: {ex}");
                }
            }
        }

        private void SaveItemsToFile()
        {
            lock (_fileLock)
            {
                try
                {
                    var options = new JsonSerializerOptions { WriteIndented = true };
                    var tmp = _itemsFilePath + ".tmp";
                    var json = JsonSerializer.Serialize(Items, options);
                    File.WriteAllText(tmp, json);
                    File.Copy(tmp, _itemsFilePath, true);
                    File.Delete(tmp);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed to save items to {_itemsFilePath}: {ex}");
                }
            }
        }


        public void AddItem(Item item)
        {
            item.UpdateDateTime = DateTime.Now;
            // make sure status list contains this status
            if (item.Status != null)
            {
                AddStatus(item.Status);
            }

            Items.Add(item);
            SaveItemsToFile();
        }

        public IEnumerable<ItemStatus> GetAllStatuses()
        {
            return _statuses;
        }

        public ItemStatus GetStatusByName(string name)
        {
            return _statuses.FirstOrDefault(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public void UpdateItemStatus(Guid itemId, string newStatus)
        {
            var item = Items.FirstOrDefault(i => i.Id == itemId);
            if (item != null)
            {
                var status = GetStatusByName(newStatus);
                if (status != null)
                {
                    item.Status = status;
                }
                else
                {
                    var custom = new ItemStatus { Name = newStatus, Description = "Custom Status" };
                    _statuses.Add(custom);
                    item.Status = custom;
                }

                item.UpdateDateTime = DateTime.Now;
                SaveItemsToFile();
            }
        }

        public void AddStatus(ItemStatus status)
        {
            if (!_statuses.Any(s => s.Name.Equals(status.Name, StringComparison.OrdinalIgnoreCase)))
            {
                _statuses.Add(status);
            }
        }

        public Item GetItemById(Guid itemId)
        {
            return Items.FirstOrDefault(i => i.Id == itemId);
        }

        public Item GetItemByName(string itemName)
        {
            return Items.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        }

        public void UpdateItem(Item updatedItem)
        {
            var existing = Items.FirstOrDefault(i => i.Id == updatedItem.Id);
            if (existing != null)
            {
                existing.Name = updatedItem.Name;
                existing.Category = updatedItem.Category;
                existing.Color = updatedItem.Color;
                existing.Size = updatedItem.Size;
                existing.ImagePath = updatedItem.ImagePath;
                existing.TimeWorn = updatedItem.TimeWorn;
                existing.Status = updatedItem.Status;
                existing.UpdateDateTime = DateTime.Now;

                // ensure statuses list contains this status
                if (existing.Status != null)
                {
                    AddStatus(existing.Status);
                }

                SaveItemsToFile();
            }
        }

        public void DeleteItem(Guid itemId)
        {
            var item = Items.FirstOrDefault(i => i.Id == itemId);
            if (item != null)
            {
                Items.Remove(item);
                SaveItemsToFile();
            }
        }
    }
}