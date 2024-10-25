using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.ValueObjects;

namespace ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Entities;

public partial class Asset : IPublishable
{
    public int Id { get; private set; }
    public AcmeAssetIdentifier AssetIdentifier { get; private set; }
    public EPublishingStatus Status { get; private set; }
    public EAssetType Type { get; private set; }
    public virtual bool Readable => false;
    public virtual bool Viewable => false;

    public Asset(EAssetType type)
    {
        AssetIdentifier = new AcmeAssetIdentifier();
        Status = EPublishingStatus.Draft;
        Type = type;
    }
    public void SendToEdit()
    {
        Status = EPublishingStatus.ReadyToEdit;
    }

    public void SendToApproval()
    {
        Status = EPublishingStatus.ReadyToApproval;
    }

    public void ApproveAndLock()
    {
        Status = EPublishingStatus.ApprovedAndLocked;
    }

    public void Reject()
    {
        Status = EPublishingStatus.Draft;
    }

    public void ReturnToEdit()
    {
        Status = EPublishingStatus.ReadyToEdit;
    }
    
    public virtual object GetContent()
    {
        return string.Empty;
    }
    
}