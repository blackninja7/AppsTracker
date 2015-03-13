﻿#region Licence
/*
  *  Author: Marko Devcic, madevcic@gmail.com
  *  Copyright: Marko Devcic, 2014
  *  Licence: http://creativecommons.org/licenses/by-nc-nd/4.0/
 */
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Input;
using AppsTracker.Data.Service;
using AppsTracker.Data.Models;
using AppsTracker.MVVM;

namespace AppsTracker.Pages.ViewModels
{
    internal sealed class Statistics_usersViewModel : ViewModelBase, ICommunicator
    {
        #region Fields
        
        private IChartService _chartService;
        
        private AllUsersModel _allUsersModel;

        private AsyncProperty<IEnumerable<AllUsersModel>> _allUsersList;

        private AsyncProperty<IEnumerable<UsageTypeSeries>> _dailyLogins;

        private ICommand _returnFromDetailedViewCommand;

        #endregion

        #region Properties

        public override string Title
        {
            get
            {
                return "USERS";
            }
        }

        public object SelectedItem
        {
            get;
            set;
        }
        
        public AllUsersModel AllUsersModel
        {
            get
            {
                return _allUsersModel;
            }
            set
            {
                _allUsersModel = value;
                PropertyChanging("AllUsersModel");
                if (_allUsersModel != null)
                    _dailyLogins.Reload();
            }
        }
        public UsageModel UsageModel
        {
            get;
            set;
        }
        public AsyncProperty<IEnumerable<AllUsersModel>> AllUsersList
        {
            get
            {
                return _allUsersList;
            }
        }
        public AsyncProperty<IEnumerable<UsageTypeSeries>> DailyLogins
        {
            get
            {
                return _dailyLogins;
            }
        }

        public ICommand ReturnFromDetailedViewCommand
        {
            get
            {
                return _returnFromDetailedViewCommand == null ? _returnFromDetailedViewCommand = new DelegateCommand(ReturnFromDetailedView) : _returnFromDetailedViewCommand;
            }
        }

        public IMediator Mediator
        {
            get { return MVVM.Mediator.Instance; }
        }

        #endregion

        public Statistics_usersViewModel()
        {            
            _chartService = ServiceFactory.Get<IChartService>();

            _allUsersList = new AsyncProperty<IEnumerable<AllUsersModel>>(GetContent, this);
            _dailyLogins = new AsyncProperty<IEnumerable<UsageTypeSeries>>(GetSubContent, this);

            Mediator.Register(MediatorMessages.RefreshLogs, new Action(ReloadAll));
        }

        #region Loader Methods

        public void ReloadAll()
        {
            _allUsersList.Reload();
            _dailyLogins.Reload();
        }

        private IEnumerable<AllUsersModel> GetContent()
        {
            return _chartService.GetAllUsers(Globals.DateFrom, Globals.DateTo);
        }

        private IEnumerable<UsageTypeSeries> GetSubContent()
        {
            var model = AllUsersModel;
            if (model == null)
                return null;

            return _chartService.GetUsageSeries(model.Username, Globals.DateFrom, Globals.DateTo);
        }

        #endregion

        private void ReturnFromDetailedView()
        {
            AllUsersModel = null;
        }
    }
}
