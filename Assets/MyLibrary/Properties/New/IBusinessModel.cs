using System;

namespace MyLibrary {
    public delegate void ModelUpdateHandler();

    public interface IBusinessModel {
        event ModelUpdateHandler ModelUpdated;
    }
}
