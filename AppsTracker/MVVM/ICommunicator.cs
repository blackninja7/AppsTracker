﻿#region Licence
/*
  *  Author: Marko Devcic, madevcic@gmail.com
  *  Copyright: Marko Devcic, 2015
  *  Licence: http://creativecommons.org/licenses/by-nc-nd/4.0/
 */
#endregion


namespace AppsTracker.MVVM
{
    public interface ICommunicator
    {
        IMediator Mediator { get; }
    }
}
