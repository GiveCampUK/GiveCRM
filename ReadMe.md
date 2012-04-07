# GiveCRM

## About GiveCRM

[GiveCRM](http://www.givecrm.org.uk/) was designed and developed at the inaugural [GiveCampUK](http://www.givecrm.org.uk/) in London, October 2011.  The aim of the project is to provide a Customer Relationship Management (CRM) system that has been explicitly designed with charities in mind, with a focus on simplicity of use.  

## Contributing to GiveCRM

If you want to contribute to the project, check out the list of open [Issues](https://github.com/givecampuk/givecrm/issues).  

You can:

 - raise an issue
 - suggest a feature for the application

If you would like to contribute code to the project:
 
  1. A bit of background reading:
    - [Setting up Git for Windows and connecting to GitHub](http://help.github.com/win-set-up-git/)
    - [The Simple Guide to Git](http://rogerdudler.github.com/git-guide/)
    - [How to GitHub: Fork, Branch, Track, Squash and Pull Request](http://gun.io/blog/how-to-github-fork-branch-and-pull-request/).
  2. Fork the repository ([how-to](http://help.github.com/fork-a-repo/))
  3. Make some changes to the code base
  4. Send us a Pull Request once you're happy with it ([how-to](http://help.github.com/send-pull-requests/))
   
We'll do a bit of a code review before accepting your patch.

### Git Flow

You will notice when you first clone the GiveCRM repository that the default branch is `develop` rather than the more usual `master`.  We use the Git Flow branching model, [first described](http://nvie.com/posts/a-successful-git-branching-model/) by [nvie](http://www.twitter.com/nvie), so `master` moves on only at specific points.  There is a set of [helper scripts](https://github.com/nvie/gitflow) that will work on both Unix-based operating systems and Windows.  

Before you make any changes to your repository, configure your clone of GiveCRM for use with Git Flow by typing `git flow init`.  Pick a feature or bug to work on and create a new branch for that work by typing `git flow feature start <featurename>`.  This will create you a new branch for your work, and you can use git as usual from this point.  

Once your feature is finished, type `git flow feature publish <featurename>`.  This will push the feature up to your `origin` repository on GitHub and you will then be able to submit a pull request to have it merged into `develop`.  

## Join the conversation

We're on Twitter ([@GiveCRM](http://www.twitter.com/givecrm/)) using the hashtag #GiveCRM.  We have a [Trello board](https://trello.com/b/gGG1duEF) for creating and detailing tasks, and a [JabbR chatroom](http://jabbr.net/#/rooms/givecrm); why not pop in and say hi?