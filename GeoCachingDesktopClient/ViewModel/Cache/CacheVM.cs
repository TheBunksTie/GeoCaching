﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Maps.MapControl.WPF;
using Microsoft.Win32;
using Swk5.GeoCaching.BusinessLogic.CacheManager;
using Swk5.GeoCaching.Desktop.ViewModel.Cache.Image;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.Desktop.ViewModel.Cache {
    public class CacheVM : AbstractViewModelBase<CacheVM> {
        private const double TOLERANCE = 0.000001;
        private readonly DomainModel.Cache cache;
        private readonly ICacheManager cacheManager;

        private readonly Location location;
        private readonly string ownerName;
        private ImageVM currentImage;
        private ICommand deleteImage;
        private ICommand newImage;
        private ICommand updateCommand;

        public CacheVM(ICacheManager cacheManager, DomainModel.Cache cache) {
            this.cacheManager = cacheManager;
            this.cache = cache;
            location = new Location(Latitude, Longitude);
            ownerName = cacheManager.GetCacheOwner(this.cache).Name;

            // create new image collection vm (includes async loading of all assignes images)
            Images = new ObservableCollection<ImageVM>();
            LoadImages();

            CurrentImage = Images.FirstOrDefault();
        }

        public ObservableCollection<ImageVM> Images { get; private set; }

        public int Id {
            get { return cache.Id; }
        }

        public string Name {
            get { return cache.Name; }
            set {
                if (cache.Name != value) {
                    cache.Name = value;
                    RaisePropertyChangedEvent(vm => vm.Name);
                }
            }
        }

        public DateTime CreationDate {
            get { return cache.CreationDate; }
        }

        public string CreationDateString {
            get { return cache.CreationDate.ToShortDateString(); }
        }

        public double CacheDifficulty {
            get { return cache.CacheDifficulty; }
            set {
                if (Math.Abs(cache.CacheDifficulty - value) > TOLERANCE) {
                    cache.CacheDifficulty = value;
                    RaisePropertyChangedEvent(vm => vm.CacheDifficulty);
                }
            }
        }

        public double TerrainDifficulty {
            get { return cache.TerrainDifficulty; }
            set {
                if (Math.Abs(cache.TerrainDifficulty - value) > TOLERANCE) {
                    cache.TerrainDifficulty = value;
                    RaisePropertyChangedEvent(vm => vm.TerrainDifficulty);
                }
            }
        }

        public string Size {
            get { return cache.Size; }
            set {
                if (cache.Size != value) {
                    cache.Size = value;
                    RaisePropertyChangedEvent(vm => vm.Size);
                }
            }
        }

        public int OwnerId {
            get { return cache.OwnerId; }
        }

        public string Owner {
            get { return ownerName; }
        }

        public double Latitude {
            get { return cache.Position.Latitude; }
            set {
                if (Math.Abs(cache.Position.Latitude - value) > TOLERANCE) {
                    GeoPosition currentPosition = cache.Position;
                    currentPosition.Latitude = value;
                    cache.Position = currentPosition;

                    // propagate to location
                    location.Latitude = value;

                    RaisePropertyChangedEvent(vm => vm.Latitude);
                }
            }
        }

        public double Longitude {
            get { return cache.Position.Longitude; }
            set {
                if (Math.Abs(cache.Position.Longitude - value) > TOLERANCE) {
                    GeoPosition currentPosition = cache.Position;
                    currentPosition.Longitude = value;
                    cache.Position = currentPosition;

                    // propagate to location
                    location.Longitude = value;

                    RaisePropertyChangedEvent(vm => vm.Longitude);
                }
            }
        }

        public string Description {
            get { return cache.Description; }
            set {
                if (cache.Description != value) {
                    cache.Description = value;
                    RaisePropertyChangedEvent(vm => vm.Description);
                }
            }
        }

        public Location Location {
            get { return location; }
        }

        public ImageVM CurrentImage {
            get { return currentImage; }
            set {
                if (currentImage != value) {
                    currentImage = value;
                    RaisePropertyChangedEvent(vm => vm.CurrentImage);
                }
            }
        }

        public ICommand UploadImageCommand {
            get { return newImage ?? (newImage = new RelayCommand(param => UploadImage())); }
        }

        public ICommand DeleteCurrentImageCommand {
            get { return deleteImage ?? (deleteImage = new RelayCommand(param => DeleteImage())); }
        }

        public ICommand UpdateCommand {
            get { return updateCommand ?? (updateCommand = new RelayCommand(param => UpdateCache())); }
        }

        private async void LoadImages() {
            IEnumerator<DomainModel.Image> e = cacheManager.GetImagesForCache(cache.Id).GetEnumerator();

            while (await Task.Factory.StartNew(() => e.MoveNext())) {
                Images.Add(new ImageVM(cacheManager, e.Current));
            }
        }

        private void UpdateCache() {
            try {
                cacheManager.UpdateExisitingCache(cache);
            }
            catch (Exception e) {
                MessageBox.Show(e.Message, "Cache manager error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UploadImage() {            
            var openFileDialog = new OpenFileDialog {DefaultExt = ".jpg", Filter = "Image Files |*.png; *.jpg; *.gif"};

            if (openFileDialog.ShowDialog() == true) {
                try {
                    DomainModel.Image image = cacheManager.UploadImage(cache.Id, openFileDialog.OpenFile(), Path.GetExtension(openFileDialog.FileName));
                    // make just uploaded image visible
                    Images.Add(new ImageVM(cacheManager, image));

                }
                catch (Exception e) {
                    MessageBox.Show(e.Message, "Cache manager error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DeleteImage() {
            if (CurrentImage != null) {
                try {
                    cacheManager.DeleteImage(CurrentImage.Image);
                    Images.Remove(CurrentImage);
                    CurrentImage = null;
                }
                catch (Exception e) {
                    MessageBox.Show(e.Message, "Cache manager error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}