using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Entities;
using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.ValueObjects;

namespace ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Aggregates;

public partial class Tutorial : IPublishable 
{
    public ICollection<Asset> Assets { get; }
    public EPublishingStatus Status { get; protected set; }
    
    public bool HasReadableAssets => Assets.Any(asset => asset.Readable);
    
    public bool HasViewableAssets => Assets.Any(asset => asset.Viewable);
    
    public bool Readable => HasReadableAssets;
    
    public bool Viewable => HasViewableAssets;
    
    public bool HasAllAssetsWithStaus(EPublishingStatus status) => Assets.All(asset => asset.Status == status);

    public bool ExistsImageByUrl(string imageUrl) =>
        Assets.Any(asset => asset.Type == EAssetType.Image && (string)asset.GetContent() == imageUrl);  
    
    private bool ExistsVideoByUrl(string videoUrl) =>
        Assets.Any(asset => asset.Type == EAssetType.Video && (string)asset.GetContent() == videoUrl);

    public bool ExistsReadableContent(string content) => Assets.Any(asset =>
        asset.Type == EAssetType.ReadableContentItem && (string)asset.GetContent() == content);

    public void AddImage(string imageUrl)
    {
        if (ExistsImageByUrl(imageUrl)) return;
        Assets.Add(new ImageAsset(imageUrl));
    }
    
    public void AddVideo(string videoUrl)
    {
        if (ExistsVideoByUrl(videoUrl)) return;
        Assets.Add(new VideoAsset(videoUrl));
    }
    
    public void AddReadableContent(string content)
    {
        if (ExistsReadableContent(content)) return;
        Assets.Add(new ReadableContentAsset(content));
    }

    public void RemoveAsset(AcmeAssetIdentifier identifier)
    {
        var asset = Assets.FirstOrDefault(a => a.AssetIdentifier == identifier);
        if( asset != null ) Assets.Remove(asset);
    }

    public void ClearAssets() => Assets.Clear();

    
    public void SendToEdit()
    {
        if(HasAllAssetsWithStaus(EPublishingStatus.ReadyToEdit))
            Status = EPublishingStatus.ReadyToEdit;
    }

    public void SendToApproval()
    {
        if(HasAllAssetsWithStaus(EPublishingStatus.ReadyToApproval))
            Status = EPublishingStatus.ReadyToApproval;
    }

    public void ApproveAndLock()
    {
        if(HasAllAssetsWithStaus(EPublishingStatus.ApprovedAndLocked))
            Status = EPublishingStatus.ApprovedAndLocked;
    }

    public void Reject()
    {
        if(HasAllAssetsWithStaus(EPublishingStatus.Draft))
            Status = EPublishingStatus.Draft;
    }

    public void ReturnToEdit()
    {
        if(HasAllAssetsWithStaus(EPublishingStatus.ReadyToEdit))
            Status = EPublishingStatus.ReadyToEdit;
    }
    
}