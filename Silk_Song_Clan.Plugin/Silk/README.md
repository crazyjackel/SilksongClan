# How Silk works

We set up a simple injected manager that reads from the savemanager. It should keep track of silk and injects into the savemanager.
We will hijack `this.permanentlyDisabledAbilities` on the savedata to encode our json. Since this data is reset on a per-run basis, we can use it as a custom resource tracker via json. All we have to do is provide our string with some magic header, search for it and load that json.

We now have a managed that can track the Silk, allowing ease of access. 

What we now need to do is adjust CanAfford and OnPlay effects.

SilkManager needs to map Hashes of CardDatas to their base silk cost and then.

We need a CardPipelineDecorator