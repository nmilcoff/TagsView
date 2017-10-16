# TagsView

[![Build Status](https://www.bitrise.io/app/14726f447bc8809f/status.svg?token=9Pt4h7jEexHPA3_V3lUBZQ&branch=master)](https://www.bitrise.io/app/14726f447bc8809f/status.svg?token=9Pt4h7jEexHPA3_V3lUBZQ&branch=master)

|Package Name|Version|
| ------------------- | :------------------: |
|TagsView|[![NuGet](https://img.shields.io/nuget/v/TagsView.svg?label=NuGet)](https://www.nuget.org/packages/TagsView/)|
|MvxTagsView|[![NuGet](https://img.shields.io/nuget/v/MvxTagsView.svg?label=NuGet)](https://www.nuget.org/packages/MvxTagsView/)|


Simple and highly customizable Xamarin.iOS tag list view. Originally inspired by https://github.com/ElaWorkshop/TagListView

Customizable features:
- Left/Center/Right alignment
- Font
- X/Y Margins
- X/Y Paddings
- Text color
- Background text color
- Tag corner radius
- Tag border color
- Tag border width
- Tag border color
- Tag controls distance
- Tag icon
- Tag icon size
- Tag icon color
- Tag selected event
- Tag button tapped event

<h3 align="center">
    <img src="Assets/sshot.png" alt="Screenshot" />
</h3>


## Download & Install

Get it on Nuget!

[TagsView](https://www.nuget.org/packages/TagsView/)

[MvxTagsView](https://www.nuget.org/packages/MvxTagsView/)

## Requirements

iOS 9+

## Features and usage

It's super simple to get started:

```c#
public class ViewController : UIViewController
{
    private TagListView tagsView;

    public override void ViewDidLoad()
    {
        // code
        this.tagsView = new TagListView()
        {
            // you can customize properties here!
        };

        this.View.AddSubview(this.tagsView);

        this.View.AddConstraints(        
            // Add your constraints!
        );

        // you can attach a source object to each tag
        var myObject = new MyModel { Title = "I'm a MyModel!" };
        this.tagsView.AddTag(myObject.Title, myObject); 

        // but, if none is provided, it will be the text string 
        this.tagsView.AddTag("I'm a simple tag!"); 
    }
}
```

By default, `TagsView` will display a button next to the text of the tag. To prevent this, you can pass the value `false` for the parameter `enableTagButton` in its constructor.

As explained in the code snippet, each tag can contain a source object. These will be available for you on the events the control exposes:
- `TagSelected`: User tapped a tag.
- `TagButtonTapped`: User tapped a tag button.


There are also a lot of properties available for you to customize the look & feel:

|Feature|Property|Type|
| ------------------- | :------------------: | :------------------: |
|Alignment|Alignment|`TagsAlignment`|
Text color|TagTextColor|`UIColor`|
Tag background color|TagBackgroundColor|`UIColor`|
Font|TextFont|`UIFont`|
Tag corner radius|CornerRadius|`float`|
Tag border width|BorderWidth|`float`|
Tag border color|BorderColor|`UIColor`|
Padding Y|PaddingY|`float`|
Padding X|PaddingX|`float`|
Margin Y|Margin Y|`float`|
Margin X|Margin X|`float`|
Distance between text and button|ControlsDistance|`float`|
Button size|TagButtonSize|`float`|
Button color|TagButtonColor|`UIColor`|
Button icon|ButtonIcon|`UIImage`|

## MvvmCross version


If you are using MvvmCross, you can take advantage of MvxTagListView!

```c#
public class ViewController : UIViewController
{
    // declare a MvxTagListView using MyObject as items type
    private MvxTagListView<MyObject> mvxTagsView;

    public override void ViewDidLoad()
    {
        // You need to specify how MyObject can be translated to a string in the ctor!
        this.mvxTagsView = new MvxTagListView<MyObject>(
            myObject => myObject.Title)
        {
            // you can customize properties here!
        };

        this.View.AddSubview(this.mvxTagsView);

        this.View.AddConstraints(        
            // Add your constraints!
        );

        var set = this.CreateBindingSet<FirstView, FirstViewModel>();
        // In this case, YourSource should be an ObservableCollection<MyObject>
        set.Bind(this.mvxTagsView).For(v => v.ItemsSource).To(vm => vm.YourSource); 
        // MyObjectTagSelectedCommand should be a IMvxCommand<MyObject>
        set.Bind(this.mvxTagsView).For(v => v.TagSelectedCommand).To(vm => vm.MyObjectTagSelectedCommand);
        // MyObjectTagButtonTappedCommand should be a IMvxCommand<MyObject>
        set.Bind(this.mvxTagsView).For(v => v.TagButtonTappedCommand).To(vm => vm.MyObjectTagButtonTappedCommand);
        set.Apply();
    }
}
```

As you can see from the code snippet, the control allows you to bind `ItemsSource` and two commands: `TagSelectedCommand`, `TagButtonTappedCommand`.

#### Using a collection of strings as items source

If your source is just a collection of strings, you should consider using `MvxSimpleTagListView`, super handy!

```c#
public class MvxSimpleTagListView : MvxTagListView<string>
    {
        public MvxSimpleTagListView(bool enableTagButton = true)
            : base(s => s, enableTagButton)
        {
        }
    }
```

## Contribution

Pull requests (and issues) are welcome!

## License

MIT